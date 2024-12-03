using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
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
            var existUser = await categoryRepository.CheckUserAsync(request.UserId, cancellationToken);
            if (!existUser)
                throw new NotFoundException("User");

            var category = new CategoryEntity(
                userId: request.UserId,
                categoryType: request.CategoryType,
                name: request.Name,
                icon: request.Icon,
                color: request.Color,
                superCategoryId: request.SuperCategoryId);

            await categoryRepository.InsertAsync(category, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return CategoryResponse.FromEntity(category);
        }
    }
}
