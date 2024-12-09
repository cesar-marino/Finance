using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Repositories
{
    public class UserRepository(FinanceContext context) : IUserRepository
    {
        public async Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
                return user != null;
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
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
                return user != null;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<UserEntity> FindAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
                return user?.ToEntity() ?? throw new NotFoundException("User");
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

        public async Task<UserEntity> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
                return user?.ToEntity() ?? throw new NotFoundException("User");
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

        public async Task<UserEntity> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
                return user?.ToEntity() ?? throw new NotFoundException("User");
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

        public async Task<IReadOnlyList<UserEntity>> FindLoggedUsersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var models = context.Users.Where(
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

        public async Task InsertAsync(UserEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = UserModel.FromEntity(aggregate);
                await context.Users.AddAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task UpdateAsync(UserEntity aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = UserModel.FromEntity(aggregate);

                var existingUser = await context.Users
                    .FirstOrDefaultAsync(x => x.UserId == user.UserId, cancellationToken: cancellationToken);

                if (existingUser != null)
                {
                    context.Entry(existingUser).CurrentValues.SetValues(user);
                    context.Users.Entry(existingUser).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }
    }
}