using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public interface IUpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, CategoryResponse>
    {
    }
}
