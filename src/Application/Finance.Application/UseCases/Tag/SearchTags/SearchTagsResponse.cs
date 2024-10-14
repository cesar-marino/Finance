using Finance.Application.Commons;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsResponse(
        int page,
        int perPage,
        int total,
        SearchOrder order,
        IReadOnlyList<TagEntity> items) : ResponseSearchPagination<TagEntity>(page, perPage, total, order, items)
    {
    }
}
