using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : IUpdateCategoryHandler
    {
        public async Task<CategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(
                id: request.CategoryId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            category.Updated(
                active: request.Active,
                categoryType: request.CategoryType,
                name: request.Name,
                icon: request.Icon,
                color: request.Color);

            await categoryRepository.UpdateAsync(
                aggregate: category,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return CategoryResponse.FromEntity(category: category);
        }
    }
}
