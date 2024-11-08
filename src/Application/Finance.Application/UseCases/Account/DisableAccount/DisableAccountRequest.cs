using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountRequest(Guid accountId) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
    }
}