using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountRequest(Guid accountId) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
    }
}