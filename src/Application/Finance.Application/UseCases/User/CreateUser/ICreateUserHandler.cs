using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.CreateUser
{
    public interface ICreateUserHandler : IRequestHandler<CreateUserRequest, UserResponse> { }
}