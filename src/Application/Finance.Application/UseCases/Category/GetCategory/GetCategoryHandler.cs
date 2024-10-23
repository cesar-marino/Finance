using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IGetCategoryHandler
    {
        public async Task<CategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.CategoryId,
                cancellationToken);

            return CategoryResponse.FromEntity(category);
        }
    }
}
