using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryRequest(Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; } = categoryId;
    }
}
