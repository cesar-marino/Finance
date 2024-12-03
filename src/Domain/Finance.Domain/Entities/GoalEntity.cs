using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class GoalEntity : AggregateRoot
    {
        public Guid UserId { get; }
        public string Name { get; private set; }
        public double ExpectedAmount { get; private set; }
        public double CurrentAmount { get; private set; }

        public GoalEntity(
            Guid userId,
            string name,
            double expectedAmount)
        {
            UserId = userId;
            Name = name;
            ExpectedAmount = expectedAmount;
            CurrentAmount = 0;
        }

        public GoalEntity(
            Guid userId,
            Guid goalId,
            string name,
            double expectedAmount,
            double currentAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(goalId, createdAt, updatedAt)
        {
            UserId = userId;
            Name = name;
            ExpectedAmount = expectedAmount;
            CurrentAmount = currentAmount;
        }

        public void Updated(string name, double expectedAmount)
        {
            Name = name;
            ExpectedAmount = expectedAmount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddAmount(double amount)
        {
            CurrentAmount += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveAmount(double amount)
        {
            CurrentAmount -= amount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}