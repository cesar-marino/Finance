using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandler(
        ITokenService tokenService,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork) : IRefreshTokenHandler
    {
        public async Task<AccountResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var username = await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);
            var account = await accountRepository.FindByUsernameAsync(username, cancellationToken);

            var accessToken = await tokenService.GenerateAccessTokenAsync(account, cancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            account.ChangeTokens(accessToken, refreshToken);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return AccountResponse.FromEntity(account);
        }
    }
}