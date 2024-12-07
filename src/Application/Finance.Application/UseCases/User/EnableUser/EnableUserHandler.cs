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
            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            user.Enable();

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}