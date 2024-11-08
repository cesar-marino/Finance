using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IDisableAccountHandler
    {
        public async Task<AccountResponse> Handle(DisableAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            account.Disable();

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}