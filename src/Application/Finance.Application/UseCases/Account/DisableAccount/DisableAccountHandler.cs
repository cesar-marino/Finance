using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandler : IDisableAccountHandler
    {
        public Task<AccountResponse> Handle(DisableAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}