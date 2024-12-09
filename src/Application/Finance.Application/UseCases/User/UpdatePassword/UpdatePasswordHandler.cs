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
            var user = await userRepository.FindAsync(
                id: request.UserId,
                cancellationToken: cancellationToken);

            var passwordIsValid = await encryptionService.VerifyAsync(
                value: request.CurrentPassword,
                hash: user.Password,
                cancellationToken: cancellationToken);

            if (!passwordIsValid)
                throw new InvalidPasswordException();

            var password = await encryptionService.EcnryptAsync(
                key: request.NewPassword,
                cancellationToken: cancellationToken);

            user.ChangePassword(password);

            await userRepository.UpdateAsync(
                aggregate: user,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return UserResponse.FromEntity(user: user);
        }
    }
}