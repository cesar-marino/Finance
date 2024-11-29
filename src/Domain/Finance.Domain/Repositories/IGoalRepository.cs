using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IGoalRepository : IRepository<GoalEntity>
    {
        Task<bool> CheckAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid accountId, Guid goalId, CancellationToken cancellationToken = default);
    }
}