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

            await tokenService.GenerateAccessTokenAsync(account, cancellationToken);
            await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}