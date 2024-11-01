namespace Finance.Infrastructure.Database.Models
{
    public class LimitModel(
        Guid accountId,
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

        public Guid AccountId { get; set; } = accountId;
        public virtual AccountModel? Account { get; set; }

        public Guid CategoryId { get; set; } = categoryId;
        public virtual CategoryModel? Category { get; set; }
    }
}
