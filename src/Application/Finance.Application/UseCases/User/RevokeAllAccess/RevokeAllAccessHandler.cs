using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.RevokeAllAccess
{
    public class RevokeAllAccessHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork) : IRevokeAllAccessHandler
    {
        public async Task Handle(RevokeAllAccessRequest request, CancellationToken cancellationToken)
        {
            var users = await userRepository.FindLoggedUsersAsync(cancellationToken);

            foreach (var user in users)
            {
                user.RevokeTokens();
                await userRepository.UpdateAsync(user, cancellationToken);
            }

            await unitOfWork.CommitAsync(cancellationToken);
            await Task.CompletedTask;
        }
    }
}