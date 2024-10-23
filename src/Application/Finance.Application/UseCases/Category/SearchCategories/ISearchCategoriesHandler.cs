using MediatR;

namespace Finance.Application.UseCases.Category.SearchCategories
{
    public interface ISearchCategoriesHandler : IRequestHandler<SearchCategoriesRequest, SearchCategoriesResponse>
    {
    }
}
