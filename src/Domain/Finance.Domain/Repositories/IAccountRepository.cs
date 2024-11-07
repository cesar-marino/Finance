using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> CheckUsernameAsync(string email, CancellationToken cancellationToken = default);
    }
}