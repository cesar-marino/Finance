using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Infrastructure.Database.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        public Task<bool> CheckUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GoalEntity> FindAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(GoalEntity aggregate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid userId, Guid goalId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResult<GoalEntity>> SearchAsync(string? name, int? currentPage, int? perPage, string? orderBy, SearchOrder? order, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(GoalEntity aggregate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}