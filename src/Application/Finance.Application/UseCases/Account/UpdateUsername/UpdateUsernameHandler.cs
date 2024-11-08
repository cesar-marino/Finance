using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.UpdateUsername
{
    public class UpdateUsernameHandler : IUpdateUsernameHandler
    {
        public Task<AccountResponse> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}