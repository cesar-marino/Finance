using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.EnableUser
{
    public class EnableUserRequest(Guid userId) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
    }
}