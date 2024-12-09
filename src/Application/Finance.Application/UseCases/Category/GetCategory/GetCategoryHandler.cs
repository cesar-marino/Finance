using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IGetCategoryHandler
    {
        public async Task<CategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                id: request.CategoryId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            var subCategories = await categoryRepository.FindSubcategoriesAsync(
                categoryId: category.Id,
                cancellationToken: cancellationToken);

            return CategoryResponse.FromEntity(
                category: category,
                subCategories: subCategories);
        }
    }
}
