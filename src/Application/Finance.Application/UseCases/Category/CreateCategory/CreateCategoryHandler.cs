using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : ICreateCategoryHandler
    {
        public async Task<CategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new CategoryEntity(
                accountId: request.AccountId,
                categoryType: request.CategoryType,
                name: request.Name,
                icon: request.Icon,
                color: request.Color);

            await categoryRepository.InsertAsync(category, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
