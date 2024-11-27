namespace Finance.Application.UseCases.Goal.Commons
{
    public class GoalResponse(
        Guid goalId,
        Guid accountId,
        string name,
        double expectedValue,
        double currentValue,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
        public double ExpectedValue { get; } = expectedValue;
        public double CurrentValue { get; } = currentValue;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;
    }
}