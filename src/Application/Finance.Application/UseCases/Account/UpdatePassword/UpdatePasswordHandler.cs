using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
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
            await encryptionService.VerifyAsync(request.CurrentPassword, account.Password, cancellationToken);

            throw new NotImplementedException();
        }
    }
}