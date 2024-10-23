using Finance.Application.Commons;
using Finance.Domain.Enums;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesRequest(
        bool? active,
        CategoryType? categoryType,
        string? name,
        int? currentPage,
        int? perPage,
        string? orderBy,
        SearchOrder? order) : SearchPaginationRequest(currentPage, perPage, orderBy, order), IRequest<SearchCategoriesResponse>
    {
        public bool? Active { get; } = active;
        public CategoryType? CategoryType { get; } = categoryType;
        public string? Name { get; } = name;
    }
}
