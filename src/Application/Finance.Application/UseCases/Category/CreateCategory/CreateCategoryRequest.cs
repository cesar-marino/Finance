using Finance.Application.UseCases.Category.Commons;
using MediatR;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryRequest(
        string name,
        string? icon,
        string? color) : IRequest<CategoryResponse>
    {
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
