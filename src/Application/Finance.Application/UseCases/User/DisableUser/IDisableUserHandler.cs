using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.DisableUser
{
    public interface IDisableUserHandler : IRequestHandler<DisableUserRequest, UserResponse> { }
}