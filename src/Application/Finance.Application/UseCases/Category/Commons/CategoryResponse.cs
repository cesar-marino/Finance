using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Application.UseCases.Category.Commons
{
    public class CategoryResponse(
        Guid accountId,
        Guid categoryId,
        bool active,
        CategoryType categoryType,
        string name,
        string? icon,
        string? color,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid AccountId { get; } = accountId;
        public Guid CategoryId { get; } = categoryId;
        public bool Active { get; } = active;
        public CategoryType CategoryType { get; } = categoryType;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static CategoryResponse FromEntity(CategoryEntity category) => new(
            accountId: category.AccountId,
            categoryId: category.Id,
            active: category.Active,
            categoryType: category.CategoryType,
            name: category.Name,
            icon: category.Icon,
            color: category.Color,
            createdAt: category.CreatedAt,
            updatedAt: category.UpdatedAt);
    }
}
