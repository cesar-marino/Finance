using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandler(ICategoryRepository categoryRepository) : IDisableCategoryHandler
    {
        public async Task<CategoryResponse> Handle(DisableCategoryRequest request, CancellationToken cancellationToken)
        {
            await categoryRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.CategoryId,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
