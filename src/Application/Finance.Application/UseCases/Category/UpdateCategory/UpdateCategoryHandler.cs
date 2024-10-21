using Finance.Application.UseCases.Category.Commons;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryHandler : IUpdateCategoryHandler
    {
        public Task<CategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
