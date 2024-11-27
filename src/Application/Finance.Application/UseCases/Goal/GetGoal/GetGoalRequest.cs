using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.GetGoal
{
    public class GetGoalRequest(Guid goalId, Guid accountId) : IRequest<GoalResponse>
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
    }
}