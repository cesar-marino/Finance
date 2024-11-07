using Finance.Domain.Entities;

namespace Finance.Application.Services
{
    public interface IAccountService
    {
        Task<AccountEntity> CreateAsync(AccountEntity account, CancellationToken cancellationToken = default);
    }
}