using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler(IAccountRepository accountRepository) : ICreateAccountHandler
    {
        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            await accountRepository.CheckEmailAsync(request.Email, cancellationToken);

            throw new NotImplementedException();
        }
    }
}