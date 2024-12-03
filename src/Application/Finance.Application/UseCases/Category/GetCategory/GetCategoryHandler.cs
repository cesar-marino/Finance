using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IGetCategoryHandler
    {
        public async Task<CategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                userId: request.UserId,
                entityId: request.CategoryId,
                cancellationToken);

            var subCategories = await categoryRepository.FindSubcategoriesAsync(category.Id, cancellationToken);

            return CategoryResponse.FromEntity(category: category, subCategories: subCategories);
        }
    }
}
