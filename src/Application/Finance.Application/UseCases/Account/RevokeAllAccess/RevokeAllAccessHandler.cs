
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandler(IAccountRepository accountRepository) : IRevokeAllAccessHandler
    {
        public async Task Handle(RevokeAllAccessRequest request, CancellationToken cancellationToken)
        {
            var accounts = await accountRepository.FindLoggedAccountsAsync(cancellationToken);

            foreach (var account in accounts)
            {
                await accountRepository.UpdateAsync(account, cancellationToken);
            }

            throw new NotImplementedException();
        }
    }
}