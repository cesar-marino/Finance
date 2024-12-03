using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalRequest(
        Guid userId,
        string name,
        double expectedAmount) : IRequest<GoalResponse>
    {
        public Guid UserId { get; } = userId;
        public string Name { get; } = name;
        public double ExpectedAmount { get; } = expectedAmount;
    }
}