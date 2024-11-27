using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class GoalEntity : AggregateRoot
    {
        public Guid AccountId { get; }
        public string Name { get; }
        public double ExpectedAmount { get; }
        public double CurrentAmount { get; }

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
    }
}