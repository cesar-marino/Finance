using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesHandler(ICategoryRepository categoryRepository) : ISearchCategoriesHandler
    {
        public async Task<SearchCategoriesResponse> Handle(SearchCategoriesRequest request, CancellationToken cancellationToken)
        {
            await categoryRepository.SearchAsync(
                active: request.Active,
                categoryType: request.CategoryType,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
