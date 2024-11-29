using MediatR;

namespace Finance.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalRequest(
        Guid goalId,
        Guid accountId) : IRequest
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
    }
}