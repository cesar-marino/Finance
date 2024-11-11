
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandler(IAccountRepository accountRepository) : IRevokeAllAccessHandler
    {
        public async Task Handle(RevokeAllAccessRequest request, CancellationToken cancellationToken)
        {
            await accountRepository.FindLoggedAccountsAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}