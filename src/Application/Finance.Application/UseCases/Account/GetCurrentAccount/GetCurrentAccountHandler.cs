using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandler(ITokenService tokenService) : IGetCurrentAccountHandler
    {
        public async Task<AccountResponse> Handle(GetCurrentAccountRequest request, CancellationToken cancellationToken)
        {
            await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);

            throw new NotImplementedException();
        }
    }
}