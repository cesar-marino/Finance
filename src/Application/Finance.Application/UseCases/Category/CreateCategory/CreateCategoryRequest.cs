using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryRequest(
        Guid accountId,
        string name,
        string? icon,
        string? color) : IRequest<CategoryResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
