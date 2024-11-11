using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IRevokeAccessHandler
    {
        public async Task<AccountResponse> Handle(RevokeAccessRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            account.RevokeTokens();

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}