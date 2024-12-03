using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.Authentication
{
    public class AuthenticationRequest(
        string email,
        string password) : IRequest<UserResponse>
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
    }
}