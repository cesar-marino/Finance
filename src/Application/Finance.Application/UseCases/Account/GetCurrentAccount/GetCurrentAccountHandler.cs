using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandler : IGetCurrentAccountHandler
    {
        public Task<AccountResponse> Handle(GetCurrentAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}