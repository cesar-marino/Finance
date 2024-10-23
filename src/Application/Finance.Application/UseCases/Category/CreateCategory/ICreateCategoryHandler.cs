using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CategoryResponse>
    {
    }
}
