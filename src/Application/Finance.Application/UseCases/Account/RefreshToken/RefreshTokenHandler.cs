using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandler(
        ITokenService tokenService,
        IAccountRepository accountRepository) : IRefreshTokenHandler
    {
        public async Task<AccountResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var username = await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);
            var account = await accountRepository.FindByUsernameAsync(username, cancellationToken);

            await tokenService.GenerateAccessTokenAsync(account, cancellationToken);
            await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}