using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandler(
        IAccountRepository accountRepository,
        IEncryptionService encryptionService) : IUpdatePasswordHandler
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

            throw new NotImplementedException();
        }
    }
}