using Finance.Application.Commons;
using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public class SearchTagsResponse(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<TagResponse> items) : SearchPaginationResponse<TagResponse>(currentPage, perPage, total, orderBy, order, items)
    {
    }
}
