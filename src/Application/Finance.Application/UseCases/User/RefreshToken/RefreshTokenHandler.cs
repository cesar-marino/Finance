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
            var username = await tokenService.GetUsernameFromTokenAsync(
                request.AccessToken,
                cancellationToken: cancellationToken);

            var user = await userRepository.FindByUsernameAsync(
                username: username,
                cancellationToken: cancellationToken);

            if (user.RefreshToken?.Value != request.RefreshToken
                || user.RefreshToken.ExpiresIn < DateTime.UtcNow)
                throw new UnauthorizedException();

            var accessToken = await tokenService.GenerateAccessTokenAsync(
                user: user,
                cancellationToken: cancellationToken);

            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken: cancellationToken);

            user.ChangeTokens(
                accessToken: accessToken,
                refreshToken: refreshToken);

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}