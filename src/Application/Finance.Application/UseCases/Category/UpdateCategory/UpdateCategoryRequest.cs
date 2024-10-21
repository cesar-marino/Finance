using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryRequest(
        Guid categoryId,
        string name,
        string? icon,
        string? color) : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; } = categoryId;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
