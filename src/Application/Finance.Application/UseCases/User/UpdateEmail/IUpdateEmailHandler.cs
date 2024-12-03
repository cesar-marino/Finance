using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdateEmail
{
    public interface IUpdateEmailHandler : IRequestHandler<UpdateEmailRequest, UserResponse> { }
}