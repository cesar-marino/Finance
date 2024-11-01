using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Infrastructure.Database.Contexts;

namespace Finance.Infrastructure.Database.Repositories
{
    public class LimitRepository(FinanceContext context) : ILimitRepository
    {
        public Task<bool> CheckAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<LimitEntity> FindAsync(Guid accountId, Guid entityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(LimitEntity aggregate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid accountId, Guid limitId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LimitEntity aggregate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
