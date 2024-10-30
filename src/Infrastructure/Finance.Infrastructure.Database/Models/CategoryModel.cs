using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Infrastructure.Database.Models
{
    public class CategoryModel(
        Guid accountId,
        Guid categoryId,
        bool active,
        CategoryType? categoryType,
        string name,
        string normalizedName,
        string? icon,
        string? color,
        Guid? superCategoryId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid CategoryId { get; set; } = categoryId;
        public bool Active { get; set; } = active;
        public CategoryType? CategoryType { get; set; } = categoryType;
        public string Name { get; set; } = name;
        public string NormalizedName { get; set; } = normalizedName;
        public string? Icon { get; set; } = icon;
        public string? Color { get; set; } = color;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime UpdatedAt { get; set; } = updatedAt;
        public Guid? SuperCategoryId { get; set; } = superCategoryId;

        public Guid AccountId { get; set; } = accountId;
        public virtual AccountModel? Account { get; set; }

        public CategoryEntity ToEntity() => new(
            accountId: AccountId,
            categoryId: CategoryId,
            active: Active,
            categoryType: CategoryType,
            name: Name,
            icon: Icon,
            color: Color,
            superCategoryId: SuperCategoryId,
            createdAt: CreatedAt,
            updatedAt: UpdatedAt);

        public static CategoryModel FromEntity(CategoryEntity category) => new(
            accountId: category.AccountId,
            categoryId: category.Id,
            active: category.Active,
            categoryType: category.CategoryType,
            name: category.Name,
            normalizedName: category.Name.Trim().ToUpper(),
            icon: category.Icon,
            color: category.Color,
            superCategoryId: category.SuperCategoryId,
            createdAt: category.CreatedAt,
            updatedAt: category.UpdatedAt);
    }
}
