using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.DisableAccount
{
    public interface IDisableAccountHandler : IRequestHandler<DisableAccountRequest, AccountResponse> { }
}