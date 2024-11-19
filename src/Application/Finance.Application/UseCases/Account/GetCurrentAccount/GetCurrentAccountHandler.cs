using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandler(
        ITokenService tokenService,
        IAccountRepository accountRepository) : IGetCurrentAccountHandler
    {
        public async Task<AccountResponse> Handle(GetCurrentAccountRequest request, CancellationToken cancellationToken)
        {
            var username = await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);
            await accountRepository.FindByUsernameAsync(username, cancellationToken);

            throw new NotImplementedException();
        }
    }
}