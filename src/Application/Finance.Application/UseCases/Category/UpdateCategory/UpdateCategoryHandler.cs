using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IUpdateCategoryHandler
    {
        public async Task<CategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.CategoryId,
                cancellationToken);

            await categoryRepository.UpdateAsync(category, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
