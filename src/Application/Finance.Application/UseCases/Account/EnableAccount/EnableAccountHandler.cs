using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountHandler : IEnableAccountHandler
    {
        public Task<AccountResponse> Handle(EnableAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}