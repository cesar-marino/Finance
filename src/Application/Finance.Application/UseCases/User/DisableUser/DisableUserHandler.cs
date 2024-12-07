using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.DisableUser
{
    public class DisableUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IDisableUserHandler
    {
        public async Task<UserResponse> Handle(DisableUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            user.Disable();

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}