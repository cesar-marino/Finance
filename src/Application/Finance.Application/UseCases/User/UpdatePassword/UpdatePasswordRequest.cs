using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordRequest(
        Guid userId,
        string currentPassword,
        string newPassword) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
        public string CurrentPassword { get; } = currentPassword;
        public string NewPassword { get; } = newPassword;
    }
}