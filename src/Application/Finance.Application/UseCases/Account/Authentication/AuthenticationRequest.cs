using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.Authentication
{
    public class AuthenticationRequest(
        string email,
        string password) : IRequest<AccountResponse>
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
    }
}