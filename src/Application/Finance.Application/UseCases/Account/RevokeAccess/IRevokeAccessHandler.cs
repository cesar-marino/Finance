using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.RevokeAccess
{
    public interface IRevokeAccessHandler : IRequestHandler<RevokeAccessRequest, AccountResponse> { }
}