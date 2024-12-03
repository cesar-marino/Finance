using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.EnableUser
{
    public class EnableUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IEnableUserHandler
    {
        public async Task<UserResponse> Handle(EnableUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            user.Enable();

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}