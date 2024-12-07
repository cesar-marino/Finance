using Finance.Application.Services;
using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.User.GetCurrentUser
{
    public class GetCurrentUserHandler(
        ITokenService tokenService,
        IUserRepository userRepository) : IGetCurrentUserHandler
    {
        public async Task<UserResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
        {
            var username = await tokenService.GetUsernameFromTokenAsync(
                token: request.AccessToken,
                cancellationToken: cancellationToken);

            var user = await userRepository.FindByUsernameAsync(
                username: username,
                cancellationToken: cancellationToken);

            return UserResponse.FromEntity(user: user);
        }
    }
}