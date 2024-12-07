using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IUserRepository : IGeneralRepository<UserEntity>
    {
        Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> CheckUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<UserEntity> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<UserEntity> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UserEntity>> FindLoggedUsersAsync(CancellationToken cancellationToken = default);
    }
}