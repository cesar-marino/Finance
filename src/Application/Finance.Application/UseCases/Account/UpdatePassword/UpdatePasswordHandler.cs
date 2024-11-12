using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandler(IAccountRepository accountRepository) : IUpdatePasswordHandler
    {
        public async Task<AccountResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            await accountRepository.FindAsync(request.AccountId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}