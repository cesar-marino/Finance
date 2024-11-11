
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandler(
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork) : IRevokeAllAccessHandler
    {
        public async Task Handle(RevokeAllAccessRequest request, CancellationToken cancellationToken)
        {
            var accounts = await accountRepository.FindLoggedAccountsAsync(cancellationToken);

            foreach (var account in accounts)
            {
                await accountRepository.UpdateAsync(account, cancellationToken);
            }

            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}