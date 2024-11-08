using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailHandler(IAccountRepository accountRepository) : IUpdateEmailHandler
    {
        public async Task<AccountResponse> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.AccountId, cancellationToken);

            await accountRepository.UpdateAsync(account, cancellationToken);

            throw new NotImplementedException();
        }
    }
}