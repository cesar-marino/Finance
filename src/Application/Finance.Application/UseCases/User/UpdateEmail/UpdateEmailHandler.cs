using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.UpdateEmail
{
    public class UpdateEmailHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IUpdateEmailHandler
    {
        public async Task<UserResponse> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
        {
            var emailInUse = await userRepository.CheckEmailAsync(request.Email, cancellationToken);
            if (emailInUse)
                throw new EmailInUseException();

            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            user.ChangeEmail(request.Email);

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}