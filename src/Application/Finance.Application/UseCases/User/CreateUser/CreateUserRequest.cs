using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.CreateUser
{
    public class CreateUserRequest(
        string username,
        string email,
        string password,
        string? phone = null) : IRequest<UserResponse>
    {
        public string Username { get; } = username;
        public string Email { get; } = email;
        public string Password { get; } = password;
        public string? Phone { get; } = phone;
    }
}