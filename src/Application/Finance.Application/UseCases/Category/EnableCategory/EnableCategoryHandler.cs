using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandler(ICategoryRepository categoryRepository) : IEnableCategoryHandler
    {
        public async Task<CategoryResponse> Handle(EnableCategoryRequest request, CancellationToken cancellationToken)
        {
            await categoryRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.CategoryId,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
