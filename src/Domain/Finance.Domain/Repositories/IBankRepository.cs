using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IBankRepository : IRepository<BankEntity>
    {
        Task<BankEntity> FindAsync(Guid bankId, CancellationToken cancellationToken = default);
    }
}