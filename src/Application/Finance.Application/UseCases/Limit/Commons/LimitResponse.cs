using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Limit.Commons
{
    public class LimitResponse(
        Guid accountId,
        Guid limitId,
        string name,
        double currentAmount,
        double limitAmount,
        CategoryLimitResponse category,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid AccountId { get; } = accountId;
        public Guid LimitId { get; } = limitId;
        public string Name { get; } = name;
        public double CurrentAmount { get; } = currentAmount;
        public double LimitAmount { get; } = limitAmount;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public virtual CategoryLimitResponse Category { get; } = category;

        public static LimitResponse FromEntity(LimitEntity limit, CategoryEntity category) => new(
            accountId: limit.AccountId,
            limit.Id,
            name: limit.Name,
            currentAmount: limit.CurrentAmount,
            limitAmount: limit.LimitAmount,
            category: category != null
                ? new CategoryLimitResponse(id: category.Id, name: category.Name, icon: category.Icon, color: category.Color)
                : new CategoryLimitResponse(id: limit.CategoryId),
            createdAt: limit.CreatedAt,
            updatedAt: limit.UpdatedAt);
    }

    public class CategoryLimitResponse(
        Guid id,
        string? name = null,
        string? icon = null,
        string? color = null)
    {
        public Guid Id { get; } = id;
        public string? Name { get; } = name;
        public string? Icon { get; } = icon;
        public string? Color { get; } = color;
    }
}
