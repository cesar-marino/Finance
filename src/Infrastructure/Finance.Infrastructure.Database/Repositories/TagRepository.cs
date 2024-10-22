using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Finance.Infrastructure.Database.Repositories
{
    public class TagRepository(FinanceContext context) : ITagRepository
    {
        public async Task<TagEntity> FindAsync(Guid accountId, Guid entityId, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await context.Tags.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.AccountId == accountId && x.TagId == entityId, cancellationToken);

                return model != null ? model.ToEntity() : throw new NotFoundException("Tag");
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

        public async Task InsertAsync(TagEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = TagModel.FromEntity(aggregate);
                await context.Tags.AddAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<SearchResult<TagEntity>> SearchAsync(
            bool? active,
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = context.Tags.AsNoTracking();

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

                List<TagModel> models;
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

        public async Task UpdateAsync(TagEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                await Task.Run(() =>
                {
                    var model = TagModel.FromEntity(aggregate);
                    var item = context.Entry(model);
                    item.State = EntityState.Modified;
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }
    }
}
