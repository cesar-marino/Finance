using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandler(ITokenService tokenService) : IRefreshTokenHandler
    {
        public async Task<AccountResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);

            throw new NotImplementedException();
        }
    }
}