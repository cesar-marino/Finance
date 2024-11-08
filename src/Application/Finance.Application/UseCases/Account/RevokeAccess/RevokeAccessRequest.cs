using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessRequest(Guid accountId) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
    }
}