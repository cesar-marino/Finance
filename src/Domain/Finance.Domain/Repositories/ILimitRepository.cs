using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ILimitRepository : IRepository<LimitEntity>
    {
        Task<bool> CheckAccountByIdAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<bool> CheckCategoryByIdAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
