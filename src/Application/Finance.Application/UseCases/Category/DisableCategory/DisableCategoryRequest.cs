using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryRequest(
        Guid userId,
        Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid CategoryId { get; } = categoryId;
    }
}
