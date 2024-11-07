using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler(IAccountRepository accountRepository) : ICreateAccountHandler
    {
        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var emailInUse = await accountRepository.CheckEmailAsync(request.Email, cancellationToken);
            if (emailInUse)
                throw new EmailInUseException();

            _ = await accountRepository.CheckUsernameAsync(request.Username, cancellationToken);

            throw new NotImplementedException();
        }
    }
}