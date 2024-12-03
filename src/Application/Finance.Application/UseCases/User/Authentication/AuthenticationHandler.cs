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
            var user = await userRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passwordIsValid = await encryptionService.VerifyAsync(request.Password, user.Password, cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            if (!user.Active)
                throw new DisableUserException();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken);
            user.ChangeTokens(accessToken, refreshToken);

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}