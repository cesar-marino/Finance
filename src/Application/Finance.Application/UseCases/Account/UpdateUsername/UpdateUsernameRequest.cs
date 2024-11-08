using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdateUsername
{
    public class UpdateUsernameRequest(
        Guid accountId,
        string username) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string Username { get; } = username;
    }
}