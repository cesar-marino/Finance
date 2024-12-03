using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IGoalRepository : IRepository<GoalEntity>
    {
        Task<bool> CheckUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid userId, Guid goalId, CancellationToken cancellationToken = default);
        Task<SearchResult<GoalEntity>> SearchAsync(
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order,
            CancellationToken cancellationToken = default);
    }
}