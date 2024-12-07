using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.UpdateUsername
{
    public class UpdateUsernameHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IUpdateUsernameHandler
    {
        public async Task<UserResponse> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
        {
            var usernameInUse = await userRepository.CheckUsernameAsync(
                username: request.Username,
                cancellationToken: cancellationToken);

            if (usernameInUse)
                throw new UsernameInUseException();

            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            user.ChangeUsername(username: request.Username);

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}