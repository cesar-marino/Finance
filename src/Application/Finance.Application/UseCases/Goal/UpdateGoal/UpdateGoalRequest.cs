using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalRequest(
        Guid goalId,
        Guid accountId,
        string name,
        double expectedAmount) : IRequest<GoalResponse>
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
        public double ExpectedAmount { get; } = expectedAmount;
    }
}