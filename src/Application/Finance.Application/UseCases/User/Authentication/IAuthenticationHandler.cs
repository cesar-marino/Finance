using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.Authentication
{
    public interface IAuthenticationHandler : IRequestHandler<AuthenticationRequest, UserResponse> { }
}