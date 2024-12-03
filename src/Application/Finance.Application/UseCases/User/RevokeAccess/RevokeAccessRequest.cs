using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.RevokeAccess
{
    public class RevokeAccessRequest(Guid userId) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
    }
}