using Finance.Application.Services;
using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.RefreshToken
{
    public class RefreshTokenHandler(
        ITokenService tokenService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IRefreshTokenHandler
    {
        public async Task<UserResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var username = await tokenService.GetUsernameFromTokenAsync(request.AccessToken, cancellationToken);
            var user = await userRepository.FindByUsernameAsync(username, cancellationToken);

            if (user.RefreshToken?.Value != request.RefreshToken
                || user.RefreshToken.ExpiresIn < DateTime.UtcNow)
                throw new UnauthorizedException();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            user.ChangeTokens(accessToken, refreshToken);

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}