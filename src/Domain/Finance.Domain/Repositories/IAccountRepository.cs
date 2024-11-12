using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        Task<AccountEntity> FindAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> CheckUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<AccountEntity> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<AccountEntity> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<AccountEntity>> FindLoggedAccountsAsync(CancellationToken cancellationToken = default);
    }
}