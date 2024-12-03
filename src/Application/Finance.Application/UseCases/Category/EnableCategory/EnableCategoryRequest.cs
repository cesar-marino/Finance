using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryRequest(
        Guid userId,
        Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid CategoryId { get; } = categoryId;
    }
}
