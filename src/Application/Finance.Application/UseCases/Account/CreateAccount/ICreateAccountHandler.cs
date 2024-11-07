using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public interface ICreateAccountHandler : IRequestHandler<CreateAccountRequest, AccountResponse> { }
}