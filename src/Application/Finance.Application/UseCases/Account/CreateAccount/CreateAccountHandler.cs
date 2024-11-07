using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler(IAccountService accountService) : ICreateAccountHandler
    {
        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = new AccountEntity(
                username: request.Username,
                email: request.Email,
                password: request.Password);

            await accountService.CreateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}