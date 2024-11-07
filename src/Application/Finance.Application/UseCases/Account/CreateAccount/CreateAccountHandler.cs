using Finance.Application.Services;
using Finance.Application.UseCases.Account.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandler(
            IAccountRepository accountRepository,
            ITokenService tokenService,
            IUnitOfWork unitOfWork) : ICreateAccountHandler
    {
        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var emailInUse = await accountRepository.CheckEmailAsync(request.Email, cancellationToken);
            if (emailInUse)
                throw new EmailInUseException();

            var usernameInUse = await accountRepository.CheckUsernameAsync(request.Username, cancellationToken);
            if (usernameInUse)
                throw new UsernameInUseException();

            var account = new AccountEntity(
                username: request.Username,
                email: request.Email,
                password: request.Password,
                phone: request.Phone);

            await tokenService.GenerateAccessTokenAsync(account, cancellationToken);
            await tokenService.GenerateRefreshTokenAsync(cancellationToken);

            await accountRepository.InsertAsync(account, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}