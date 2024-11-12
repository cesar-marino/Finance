using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public interface IUpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, AccountResponse> { }
}