using Finance.Application.Services;
using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.CreateUser
{
    public class CreateUserHandler(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            ITokenService tokenService,
            IUnitOfWork unitOfWork) : ICreateUserHandler
    {
        public async Task<UserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var emailInUse = await userRepository.CheckEmailAsync(request.Email, cancellationToken);
            if (emailInUse)
                throw new EmailInUseException();

            var usernameInUse = await userRepository.CheckUsernameAsync(request.Username, cancellationToken);
            if (usernameInUse)
                throw new UsernameInUseException();

            var password = await encryptionService.EcnryptAsync(
                request.Password,
                cancellationToken);

            var user = new UserEntity(
                username: request.Username,
                email: request.Email,
                password: password,
                phone: request.Phone);

            var accessToken = await tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            user.ChangeTokens(accessToken, refreshToken);

            await userRepository.InsertAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}