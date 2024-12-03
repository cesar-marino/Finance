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
            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            user.Disable();

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}