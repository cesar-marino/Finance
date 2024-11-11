using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandler(IAccountRepository accountRepository) : IAuthenticationHandler
    {
        public async Task<AccountResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            await accountRepository.FindByEmailAsync(request.Email, cancellationToken);

            throw new NotImplementedException();
        }
    }
}