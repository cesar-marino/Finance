using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public interface IDisableCategoryHandler : IRequestHandler<DisableCategoryRequest, CategoryResponse>
    {
    }
}
