using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : IEnableCategoryHandler
    {
        public async Task<CategoryResponse> Handle(EnableCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                id: request.CategoryId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            category.Enable();

            await categoryRepository.UpdateAsync(
                aggregate: category,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return CategoryResponse.FromEntity(category: category);
        }
    }
}
