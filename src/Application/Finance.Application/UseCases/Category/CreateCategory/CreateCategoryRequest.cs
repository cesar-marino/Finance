using Finance.Application.UseCases.Category.Commons;
using Finance.Domain.Enums;
using MediatR;

namespace Finance.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryRequest(
        Guid userId,
        CategoryType categoryType,
        string name,
        string? icon,
        string? color,
        Guid? superCategoryId) : IRequest<CategoryResponse>
    {
        public Guid UserId { get; } = userId;
        public CategoryType CategoryType { get; } = categoryType;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
        public Guid? SuperCategoryId { get; } = superCategoryId;
    }
}
