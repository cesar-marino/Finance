using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IUpdateEmailHandler
    {
        public async Task<AccountResponse> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
        {
            var emailInUse = await accountRepository.CheckEmailAsync(request.Email, cancellationToken);
            if (emailInUse)
                throw new EmailInUseException();

            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            account.ChangeEmail(request.Email);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}