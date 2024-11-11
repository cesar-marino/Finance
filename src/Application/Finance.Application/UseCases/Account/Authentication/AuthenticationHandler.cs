using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        public Task<AccountResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}