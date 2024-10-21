using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Category.Commons
{
    public class CategoryResponse(
        Guid categoryId,
        bool active,
        string name,
        string? icon,
        string? color,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid CategoryId { get; } = categoryId;
        public bool Active { get; } = active;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static CategoryResponse FromEntity(CategoryEntity category) => new(
                categoryId: category.Id,
                active: category.Active,
                name: category.Name,
                icon: category.Icon,
                color: category.Color,
                createdAt: category.CreatedAt,
                updatedAt: category.UpdatedAt);
    }
}
