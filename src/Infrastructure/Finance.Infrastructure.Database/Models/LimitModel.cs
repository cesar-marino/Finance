using Finance.Domain.Entities;

namespace Finance.Infrastructure.Database.Models
{
    public class LimitModel(
        Guid userId,
        Guid limitId,
        Guid categoryId,
        string name,
        double limitAmount,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid LimitId { get; set; } = limitId;
        public string Name { get; set; } = name;
        public double LimitAmount { get; set; } = limitAmount;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime UpdatedAt { get; set; } = updatedAt;

        public Guid UserId { get; set; } = userId;
        public virtual UserModel? User { get; set; }

        public Guid CategoryId { get; set; } = categoryId;
        public virtual CategoryModel? Category { get; set; }

        public static LimitModel FromEntity(LimitEntity limit) => new(
            userId: limit.UserId,
            limitId: limit.Id,
            categoryId: limit.CategoryId,
            name: limit.Name,
            limitAmount: limit.LimitAmount,
            createdAt: limit.CreatedAt,
            updatedAt: limit.UpdatedAt);

        public LimitEntity ToEntity() => new(
            limitId: LimitId,
            userId: UserId,
            categoryId: CategoryId,
            name: Name,
            limitAmount: LimitAmount,
            createdAt: CreatedAt,
            updatedAt: UpdatedAt);
    }
}
