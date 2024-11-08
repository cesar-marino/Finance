using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.UpdateUsername
{
    public class UpdateUsernameHandler(IAccountRepository accountRepository) : IUpdateUsernameHandler
    {
        public async Task<AccountResponse> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}