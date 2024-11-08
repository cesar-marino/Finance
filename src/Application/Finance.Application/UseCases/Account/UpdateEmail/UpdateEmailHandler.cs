using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailHandler : IUpdateEmailHandler
    {
        public Task<AccountResponse> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}