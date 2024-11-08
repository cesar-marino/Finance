using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailRequest(Guid accountId, string email) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string Email { get; } = email;
    }
}