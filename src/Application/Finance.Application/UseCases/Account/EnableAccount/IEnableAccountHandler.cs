using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.EnableAccount
{
    public interface IEnableAccountHandler : IRequestHandler<EnableAccountRequest, AccountResponse> { }
}