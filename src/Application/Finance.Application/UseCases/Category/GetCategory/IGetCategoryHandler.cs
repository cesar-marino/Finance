using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public interface IGetCategoryHandler : IRequestHandler<GetCategoryRequest, CategoryResponse>
    {
    }
}
