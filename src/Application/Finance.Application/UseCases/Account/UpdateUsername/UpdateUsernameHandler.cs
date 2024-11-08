using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
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
            var usernameInUse = await accountRepository.CheckUsernameAsync(request.Username, cancellationToken);

            if (usernameInUse)
                throw new UsernameInUseException();

            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            account.ChangeUsername(request.Username);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}