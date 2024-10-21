using Finance.Application.UseCases.Category.Commons;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandler : IEnableCategoryHandler
    {
        public Task<CategoryResponse> Handle(EnableCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
