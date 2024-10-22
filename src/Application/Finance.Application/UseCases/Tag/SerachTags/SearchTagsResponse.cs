using Finance.Application.Commons;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public class SearchTagsResponse(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<TagEntity> items) : SearchPaginationResponse<TagEntity>(currentPage, perPage, total, orderBy, order, items)
    {
    }
}
