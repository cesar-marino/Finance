using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountHandler(IAccountRepository accountRepository) : IEnableAccountHandler
    {
        public async Task<AccountResponse> Handle(EnableAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}