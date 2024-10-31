using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ILimitRepository : IRepository<LimitEntity>
    {
        Task<bool> CheckAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<bool> CheckCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid accountId, Guid limitId, CancellationToken cancellationToken = default);
    }
}
