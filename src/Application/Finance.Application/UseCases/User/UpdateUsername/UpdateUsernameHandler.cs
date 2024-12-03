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
            var usernameInUse = await userRepository.CheckUsernameAsync(request.Username, cancellationToken);

            if (usernameInUse)
                throw new UsernameInUseException();

            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            user.ChangeUsername(request.Username);

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}