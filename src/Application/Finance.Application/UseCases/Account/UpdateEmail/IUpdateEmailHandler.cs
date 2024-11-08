using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdateEmail
{
    public interface IUpdateEmailHandler : IRequestHandler<UpdateEmailRequest, AccountResponse> { }
}