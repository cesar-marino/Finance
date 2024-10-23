using Finance.Application.Commons;
using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesResponse(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<CategoryResponse> items) : SearchPaginationResponse<CategoryResponse>(currentPage, perPage, total, orderBy, order, items)
    {
    }
}
