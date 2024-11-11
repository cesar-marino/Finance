using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandler(
        IAccountRepository accountRepository,
        IEncryptionService encryptionService,
        ITokenService tokenService,
        IUnitOfWork unitOfWork) : IAuthenticationHandler
    {
        public async Task<AccountResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passwordIsValid = await encryptionService.VerifyAsync(request.Password, account.Password, cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            if (!account.Active)
                throw new DisableAccountException();

            await tokenService.GenerateAccessTokenAsync(account, cancellationToken);
            await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}