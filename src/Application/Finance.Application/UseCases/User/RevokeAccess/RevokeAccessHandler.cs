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
            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            user.RevokeTokens();

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}