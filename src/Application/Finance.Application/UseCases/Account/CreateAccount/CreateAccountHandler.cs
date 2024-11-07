using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler : ICreateAccountHandler
    {
        public Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}