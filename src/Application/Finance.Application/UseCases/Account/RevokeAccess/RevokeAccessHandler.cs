using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessHandler : IRevokeAccessHandler
    {
        public Task<AccountResponse> Handle(RevokeAccessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}