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
            var emailInUse = await userRepository.CheckEmailAsync(
                email: request.Email,
                cancellationToken: cancellationToken);

            if (emailInUse)
                throw new EmailInUseException();

            var usernameInUse = await userRepository.CheckUsernameAsync(
                username: request.Username,
                cancellationToken: cancellationToken);

            if (usernameInUse)
                throw new UsernameInUseException();

            var password = await encryptionService.EcnryptAsync(
                key: request.Password,
                cancellationToken: cancellationToken);

            var user = new UserEntity(
                username: request.Username,
                email: request.Email,
                password: password,
                phone: request.Phone);

            var accessToken = await tokenService.GenerateAccessTokenAsync(
                user: user,
                cancellationToken: cancellationToken);

            var refreshToken = await tokenService.GenerateRefreshTokenAsync(cancellationToken: cancellationToken);

            user.ChangeTokens(
                accessToken: accessToken,
                refreshToken: refreshToken);

            await userRepository.InsertAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}