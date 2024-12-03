using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdateEmail
{
    public class UpdateEmailRequest(Guid userId, string email) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
        public string Email { get; } = email;
    }
}