using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandler(IAccountRepository accountRepository) : IDisableAccountHandler
    {
        public async Task<AccountResponse> Handle(DisableAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}