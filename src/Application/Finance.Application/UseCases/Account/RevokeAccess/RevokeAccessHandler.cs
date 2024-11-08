using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessHandler(IAccountRepository accountRepository) : IRevokeAccessHandler
    {
        public async Task<AccountResponse> Handle(RevokeAccessRequest request, CancellationToken cancellationToken)
        {
            await accountRepository.FindAsync(request.AccountId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}