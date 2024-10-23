using Finance.Application.UseCases.Category.Commons;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandler : ICreateCategoryHandler
    {
        public Task<CategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
