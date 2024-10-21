using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandler(ICategoryRepository categoryRepository) : ICreateCategoryHandler
    {
        public async Task<CategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new CategoryEntity(
                name: request.Name,
                icon: request.Icon,
                color: request.Color);

            await categoryRepository.InsertAsync(category, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
