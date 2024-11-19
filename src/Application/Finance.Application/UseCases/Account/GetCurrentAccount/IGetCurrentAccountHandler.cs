using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.GetCurrentAccount
{
    public interface IGetCurrentAccountHandler : IRequestHandler<GetCurrentAccountRequest, AccountResponse>
    {

    }
}