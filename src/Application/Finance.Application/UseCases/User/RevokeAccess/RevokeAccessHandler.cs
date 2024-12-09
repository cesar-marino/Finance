using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.RevokeAccess
{
    public class RevokeAccessHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IRevokeAccessHandler
    {
        public async Task<UserResponse> Handle(RevokeAccessRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            user.RevokeTokens();

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}