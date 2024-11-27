using Finance.Application.UseCases.Goal.Commons;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandler : IUpdateGoalHandler
    {
        public Task<GoalResponse> Handle(UpdateGoalRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}