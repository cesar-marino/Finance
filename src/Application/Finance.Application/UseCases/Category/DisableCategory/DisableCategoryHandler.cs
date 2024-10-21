using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : IDisableCategoryHandler
    {
        public async Task<CategoryResponse> Handle(DisableCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.CategoryId, cancellationToken);

            await categoryRepository.UpdateAsync(category, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
