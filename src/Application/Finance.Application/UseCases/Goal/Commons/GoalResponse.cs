using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Goal.Commons
{
    public class GoalResponse(
        Guid goalId,
        Guid accountId,
        string name,
        double expectedAmount,
        double currentAmount,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
        public double ExpectedAmount { get; } = expectedAmount;
        public double CurrentAmount { get; } = currentAmount;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static GoalResponse FromEntity(GoalEntity goal) => new(
            goalId: goal.Id,
            accountId: goal.AccountId,
            name: goal.Name,
            expectedAmount: goal.ExpectedAmount,
            currentAmount: goal.CurrentAmount,
            createdAt: goal.CreatedAt,
            updatedAt: goal.UpdatedAt);
    }
}