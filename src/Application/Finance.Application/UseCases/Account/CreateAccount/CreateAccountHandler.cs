using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler(IAccountService accountService) : ICreateAccountHandler
    {
        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            await accountService.CreateAsync();

            throw new NotImplementedException();
        }
    }
}