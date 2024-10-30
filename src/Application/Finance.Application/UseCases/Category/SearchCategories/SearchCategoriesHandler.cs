using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesHandler(ICategoryRepository categoryRepository) : ISearchCategoriesHandler
    {
        public async Task<SearchCategoriesResponse> Handle(SearchCategoriesRequest request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.SearchAsync(
                active: request.Active,
                categoryType: request.CategoryType,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order,
                cancellationToken);

            return new(
                currentPage: result.CurrentPage,
                perPage: result.PerPage,
                total: result.Total,
                orderBy: result.OrderBy,
                order: result.Order,
                items: result.Items.Select(x => CategoryResponse.FromEntity(x)).ToList());
        }
    }
}
