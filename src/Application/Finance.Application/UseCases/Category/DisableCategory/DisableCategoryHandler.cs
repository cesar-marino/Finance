using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandler(ICategoryRepository categoryRepository) : IDisableCategoryHandler
    {
        public async Task<CategoryResponse> Handle(DisableCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.CategoryId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
