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
            var emailInUse = await userRepository.CheckEmailAsync(
                email: request.Email,
                cancellationToken: cancellationToken);

            if (emailInUse)
                throw new EmailInUseException();

            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            user.ChangeEmail(request.Email);

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}