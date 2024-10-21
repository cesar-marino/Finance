using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryRequest(Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; } = categoryId;
    }
}
