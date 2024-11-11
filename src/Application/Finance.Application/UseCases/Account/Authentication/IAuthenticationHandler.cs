using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.Authentication
{
    public interface IAuthenticationHandler : IRequestHandler<AuthenticationRequest, AccountResponse> { }
}