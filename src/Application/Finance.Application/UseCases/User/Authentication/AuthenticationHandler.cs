using Finance.Application.Services;
using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.Authentication
{
    public class AuthenticationHandler(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        ITokenService tokenService,
        IUnitOfWork unitOfWork) : IAuthenticationHandler
    {
        public async Task<UserResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindByEmailAsync(
                email: request.Email,
                cancellationToken: cancellationToken);

            var passwordIsValid = await encryptionService.VerifyAsync(
                value: request.Password,
                hash: user.Password,
                cancellationToken: cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            if (!user.Active)
                throw new DisableUserException();

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