using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryRequest(
        Guid userId,
        Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid CategoryId { get; } = categoryId;
    }
}
