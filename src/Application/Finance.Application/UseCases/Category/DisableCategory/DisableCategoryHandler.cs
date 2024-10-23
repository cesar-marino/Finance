using Finance.Application.UseCases.Category.Commons;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandler : IDisableCategoryHandler
    {
        public Task<CategoryResponse> Handle(DisableCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
