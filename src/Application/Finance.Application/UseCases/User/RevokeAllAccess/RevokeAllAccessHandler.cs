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
            var users = await userRepository.FindLoggedUsersAsync(cancellationToken: cancellationToken);

            foreach (var user in users)
            {
                user.RevokeTokens();

                await userRepository.UpdateAsync(
                    aggregate: user,
                    cancellationToken: cancellationToken);
            }

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            await Task.CompletedTask;
        }
    }
}