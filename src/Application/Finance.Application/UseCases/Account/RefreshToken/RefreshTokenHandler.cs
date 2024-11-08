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
            await accountRepository.FindByUsernameAsync(username, cancellationToken);

            throw new NotImplementedException();
        }
    }
}