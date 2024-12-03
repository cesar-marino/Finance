using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.DisableUser
{
    public class DisableUserRequest(Guid userId) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
    }
}