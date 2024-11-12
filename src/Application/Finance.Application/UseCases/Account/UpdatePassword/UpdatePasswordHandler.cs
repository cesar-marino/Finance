using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandler(
        IAccountRepository accountRepository,
        IEncryptionService encryptionService,
        IUnitOfWork unitOfWork) : IUpdatePasswordHandler
    {
        public async Task<AccountResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);
            var passwordIsValid = await encryptionService.VerifyAsync(
                request.CurrentPassword,
                account.Password,
                cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            _ = await encryptionService.EcnryptAsync(request.NewPassword, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}