using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandler : IUpdatePasswordHandler
    {
        public Task<AccountResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}