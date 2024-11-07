using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountRequest(
        string username,
        string email,
        string passwrd) : IRequest<AccountResponse>
    {
        public string Username { get; } = username;
        public string Email { get; } = email;
        public string Password { get; } = passwrd;
    }
}