using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IEnableAccountHandler
    {
        public async Task<AccountResponse> Handle(EnableAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            account.Enable();

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}