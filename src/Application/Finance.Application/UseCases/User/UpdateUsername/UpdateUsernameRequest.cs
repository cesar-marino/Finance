using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdateUsername
{
    public class UpdateUsernameRequest(
        Guid userId,
        string username) : IRequest<UserResponse>
    {
        public Guid UserId { get; } = userId;
        public string Username { get; } = username;
    }
}