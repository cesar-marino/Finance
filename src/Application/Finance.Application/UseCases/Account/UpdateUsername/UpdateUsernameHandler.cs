using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.UpdateUsername
{
    public class UpdateUsernameHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IUpdateUsernameHandler
    {
        public async Task<AccountResponse> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}