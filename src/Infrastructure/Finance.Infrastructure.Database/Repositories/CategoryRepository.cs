using Finance.Domain.Entities;
using Finance.Domain.Enums;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Repositories
{
    public class CategoryRepository(FinanceContext context) : ICategoryRepository
    {
        public async Task<bool> CheckUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
                return user != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<CategoryEntity> FindAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await context.Categories.FirstOrDefaultAsync(
                    x => x.UserId == userId && x.CategoryId == id,
                    cancellationToken);

                return model?.ToEntity() ?? throw new NotFoundException("Category");
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<IReadOnlyList<CategoryEntity>> FindSubcategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            try
            {
                var models = await context.Categories.Where(x => x.SuperCategoryId == categoryId).ToListAsync(cancellationToken);
                return models.Select(x => x.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task InsertAsync(CategoryEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = CategoryModel.FromEntity(aggregate);
                await context.Categories.AddAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<SearchResult<CategoryEntity>> SearchAsync(
            bool? active,
            CategoryType? categoryType,
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = context.Categories.AsNoTracking();

                if (active is not null)
                    query = query.Where(x => x.Active == active);

                if (name is not null)
                    query = query.Where(x => x.NormalizedName.Contains(name.Trim(), StringComparison.CurrentCultureIgnoreCase));

                query = (orderBy?.ToLower(), order) switch
                {
                    ("active", SearchOrder.Asc) => query.OrderBy(x => x.Active),
                    ("active", SearchOrder.Desc) => query.OrderByDescending(x => x.Active),
                    ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.NormalizedName),
                    ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                    ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                    ("updatedat", SearchOrder.Asc) => query.OrderBy(x => x.UpdatedAt),
                    ("updatedat", SearchOrder.Desc) => query.OrderByDescending(x => x.UpdatedAt),
                    _ => query.OrderBy(x => x.NormalizedName),
                };

                List<CategoryModel> models;
                if (currentPage != null && perPage != null)
                {
                    _ = int.TryParse(currentPage.ToString(), out int skip);
                    _ = int.TryParse(perPage.ToString(), out int take);

                    models = await query
                        .Skip((skip - 1) * take)
                        .Take(take)
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    models = await query.ToListAsync(cancellationToken);
                }

                return new(
                    currentPage: currentPage,
                    perPage: perPage,
                    total: models.Count,
                    orderBy: orderBy,
                    order: order,
                    items: models.Select(x => x.ToEntity()).ToList());
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task UpdateAsync(CategoryEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var category = CategoryModel.FromEntity(aggregate);

                var existingCategory = await context.Categories
                    .FirstOrDefaultAsync(x => x.UserId == category.UserId && x.CategoryId == category.CategoryId, cancellationToken: cancellationToken);

                if (existingCategory != null)
                {
                    context.Entry(existingCategory).CurrentValues.SetValues(category);
                    context.Categories.Entry(existingCategory).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }
    }
}
