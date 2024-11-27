using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class GoalEntity : AggregateRoot
    {
        public Guid AccountId { get; }
        public string Name { get; private set; }
        public double ExpectedAmount { get; private set; }
        public double CurrentAmount { get; private set; }

        public GoalEntity(
            Guid accountId,
            string name,
            double expectedAmount)
        {
            AccountId = accountId;
            Name = name;
            ExpectedAmount = expectedAmount;
            CurrentAmount = 0;
        }

        public GoalEntity(
            Guid accountId,
            Guid goalId,
            string name,
            double expectedAmount,
            double currentAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(goalId, createdAt, updatedAt)
        {
            AccountId = accountId;
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
    }
}