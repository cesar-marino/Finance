using Finance.Application.Services;
using Finance.Application.UseCases.User.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordHandler(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        IUnitOfWork unitOfWork) : IUpdatePasswordHandler
    {
        public async Task<UserResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.UserId, cancellationToken);
            var passwordIsValid = await encryptionService.VerifyAsync(
                request.CurrentPassword,
                user.Password,
                cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            var password = await encryptionService.EcnryptAsync(request.NewPassword, cancellationToken);
            user.ChangePassword(password);

            await userRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return UserResponse.FromEntity(user);
        }
    }
}