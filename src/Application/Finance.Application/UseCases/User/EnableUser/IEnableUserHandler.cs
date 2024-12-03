using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.EnableUser
{
    public interface IEnableUserHandler : IRequestHandler<EnableUserRequest, UserResponse> { }
}