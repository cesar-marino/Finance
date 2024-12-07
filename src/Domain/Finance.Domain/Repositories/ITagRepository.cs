using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ITagRepository : IAuditableRepository<TagEntity>
    {
        Task<SearchResult<TagEntity>> SearchAsync(
            bool? active,
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order,
            CancellationToken cancellationToken = default);
    }
}
