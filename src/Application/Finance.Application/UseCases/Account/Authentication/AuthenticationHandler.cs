using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandler(
        IAccountRepository accountRepository,
        IEncryptionService encryptionService) : IAuthenticationHandler
    {
        public async Task<AccountResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passwordIsValid = await encryptionService.VerifyAsync(request.Password, account.Password, cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            throw new NotImplementedException();
        }
    }
}