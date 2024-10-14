using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ITagRepository : IRepository<TagEntity>
    {
        Task<SearchResponse<TagEntity>> SearchAsync(
            int page,
            int perPage,
            bool? active = null,
            string? name = null,
            SearchOrder order = SearchOrder.Asc,
            CancellationToken cancellationToken = default);
    }
}
