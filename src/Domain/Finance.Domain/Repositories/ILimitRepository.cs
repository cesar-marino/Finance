using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ILimitRepository : IAuditableRepository<LimitEntity>
    {
        Task<bool> CheckUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> CheckCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid userId, Guid limitId, CancellationToken cancellationToken = default);
    }
}
