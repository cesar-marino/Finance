using System.Security.Cryptography.X509Certificates;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Repositories
{
    public class AccountRepository(FinanceContext context) : IAccountRepository
    {
        public async Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
                return account != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<bool> CheckUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
                return account != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<AccountEntity> FindAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId, cancellationToken);
                return account?.ToEntity() ?? throw new NotFoundException("Account");
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public Task<AccountEntity> FindAsync(Guid accountId, Guid entityId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountEntity> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
                return account?.ToEntity() ?? throw new NotFoundException("Account");
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<AccountEntity> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var account = await context.Accounts.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
                return account?.ToEntity() ?? throw new NotFoundException("Account");
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<IReadOnlyList<AccountEntity>> FindLoggedAccountsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var models = context.Accounts.Where(
                    x => x.AccessTokenValue != null
                        && x.AccessTokenExpiresIn != null
                        && x.RefreshTokenValue != null
                        && x.RefreshTokenExpiresIn != null);

                return await models.Select(x => x.ToEntity()).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task InsertAsync(AccountEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = AccountModel.FromEntity(aggregate);
                await context.Accounts.AddAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task UpdateAsync(AccountEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                await Task.Run(() =>
                {
                    var model = AccountModel.FromEntity(aggregate);
                    var item = context.Entry(model);
                    item.State = EntityState.Modified;
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }
    }
}