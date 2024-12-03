using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Application.UseCases.Category.Commons
{
    public class CategoryResponse(
        Guid userId,
        Guid categoryId,
        bool active,
        CategoryType? categoryType,
        string name,
        string? icon,
        string? color,
        DateTime createdAt,
        DateTime updatedAt,
        AggregateCategoryResponse? superCategory = null,
        IReadOnlyList<AggregateCategoryResponse>? subCategories = null)
    {
        public Guid UserId { get; } = userId;
        public Guid CategoryId { get; } = categoryId;
        public bool Active { get; } = active;
        public CategoryType? CategoryType { get; } = categoryType;
        public string Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public virtual AggregateCategoryResponse? SuperCategory { get; } = superCategory;
        public virtual IReadOnlyList<AggregateCategoryResponse>? SubCategories { get; } = subCategories;

        public static CategoryResponse FromEntity(
            CategoryEntity category,
            CategoryEntity? superCategory = null,
            IReadOnlyList<CategoryEntity>? subCategories = null) => new(
                userId: category.UserId,
                categoryId: category.Id,
                active: category.Active,
                categoryType: category.CategoryType,
                name: category.Name,
                icon: category.Icon,
                color: category.Color,
                createdAt: category.CreatedAt,
                updatedAt: category.UpdatedAt,
                superCategory: superCategory != null ?
                    new AggregateCategoryResponse(id: superCategory.Id, name: superCategory.Name, icon: superCategory.Icon, color: superCategory.Color) :
                    new AggregateCategoryResponse(id: category.SuperCategoryId),
                subCategories: subCategories?.Select(x => new AggregateCategoryResponse(id: x.Id, name: x.Name, icon: x.Icon, color: x.Color)).ToList());
    }

    public class AggregateCategoryResponse(
        Guid? id,
        string? name = null,
        string? icon = null,
        string? color = null)
    {
        public Guid? Id { get; } = id;
        public string? Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
