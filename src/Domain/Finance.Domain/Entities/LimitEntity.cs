using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class LimitEntity : AuditableAggregateRoot
    {
        public string Name { get; private set; }
        public double LimitAmount { get; private set; }
        public Guid CategoryId { get; private set; }

        public LimitEntity(
            Guid userId,
            Guid categoryId,
            string name,
            double limitAmount) : base(userId: userId)
        {
            CategoryId = categoryId;
            Name = name;
            LimitAmount = limitAmount;
        }

        public LimitEntity(
            Guid limitId,
            Guid userId,
            Guid categoryId,
            string name,
            double limitAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(id: limitId, userId: userId, createdAt: createdAt, updatedAt: updatedAt)
        {
            CategoryId = categoryId;
            Name = name;
            LimitAmount = limitAmount;
        }

        public void Update(string name, double limitAmount, Guid categoryId)
        {
            Name = name;
            LimitAmount = limitAmount;
            CategoryId = categoryId;
        }
    }
}
