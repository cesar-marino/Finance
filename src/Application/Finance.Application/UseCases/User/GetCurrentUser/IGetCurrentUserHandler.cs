using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.GetCurrentUser
{
    public interface IGetCurrentUserHandler : IRequestHandler<GetCurrentUserRequest, UserResponse>
    {

    }
}