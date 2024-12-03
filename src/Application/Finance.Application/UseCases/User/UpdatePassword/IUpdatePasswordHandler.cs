using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdatePassword
{
    public interface IUpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, UserResponse> { }
}