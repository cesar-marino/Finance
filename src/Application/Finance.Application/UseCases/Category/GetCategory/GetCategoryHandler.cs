using Finance.Application.UseCases.Category.Commons;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandler : IGetCategoryHandler
    {
        public Task<CategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
