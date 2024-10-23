using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public interface IEnableCategoryHandler : IRequestHandler<EnableCategoryRequest, CategoryResponse>
    {
    }
}
