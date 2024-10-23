using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.GetCategory
{
    public class GetCategoryRequest(
        Guid accountId,
        Guid categoryId) : IRequest<CategoryResponse>
    {
        public Guid AccountId { get; } = accountId;
        public Guid CategoryId { get; } = categoryId;
    }
}
