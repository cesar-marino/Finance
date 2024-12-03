using MediatR;

namespace Finance.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalRequest(
        Guid goalId,
        Guid userId) : IRequest
    {
        public Guid GoalId { get; } = goalId;
        public Guid UserId { get; } = userId;
    }
}