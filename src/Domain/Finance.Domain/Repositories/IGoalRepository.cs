using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IGoalRepository : IRepository<GoalEntity>
    {
        Task<bool> CheckAccountAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}