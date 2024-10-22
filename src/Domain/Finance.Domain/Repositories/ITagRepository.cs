using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface ITagRepository : IRepository<TagEntity>
    {
        Task<SearchResult<TagEntity>> SearchAsync(
            bool? active,
            string? name,
            int currentPage,
            int perPage,
            SearchOrder order);
    }
}
