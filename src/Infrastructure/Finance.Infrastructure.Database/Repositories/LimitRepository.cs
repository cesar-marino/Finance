using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Repositories
{
    public class LimitRepository(FinanceContext context) : ILimitRepository
    {
        public async Task<bool> CheckAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId, cancellationToken);
                return account != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<bool> CheckCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
                return category != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<LimitEntity> FindAsync(Guid accountId, Guid entityId, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await context.Limits.FirstOrDefaultAsync(x => x.AccountId == accountId && x.LimitId == entityId, cancellationToken);
                return model?.ToEntity() ?? throw new NotFoundException("Limit");
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

        public async Task InsertAsync(LimitEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = LimitModel.FromEntity(aggregate);
                await context.Limits.AddAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task RemoveAsync(Guid accountId, Guid limitId, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = await context.Limits.FirstOrDefaultAsync(x => x.AccountId == accountId && x.LimitId == limitId, cancellationToken)
                    ?? throw new NotFoundException("Limit");

                context.Limits.Remove(model);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task UpdateAsync(LimitEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                await Task.Run(() =>
                {
                    var model = LimitModel.FromEntity(aggregate);
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
