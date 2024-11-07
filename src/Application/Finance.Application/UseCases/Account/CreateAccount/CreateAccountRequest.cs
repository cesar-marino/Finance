using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountRequest(
        string username,
        string email,
        string password,
        string? phone = null) : IRequest<AccountResponse>
    {
        public string Username { get; } = username;
        public string Email { get; } = email;
        public string Password { get; } = password;
        public string? Phone { get; } = phone;
    }
}