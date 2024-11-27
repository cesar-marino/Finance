using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalRequest(
        Guid accountId,
        string name,
        double expectedAmount) : IRequest<GoalResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
        public double ExpectedAmount { get; } = expectedAmount;
    }
}