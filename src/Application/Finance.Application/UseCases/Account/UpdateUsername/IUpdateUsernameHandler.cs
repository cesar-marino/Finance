using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdateUsername
{
    public interface IUpdateUsernameHandler : IRequestHandler<UpdateUsernameRequest, AccountResponse>
    {

    }
}