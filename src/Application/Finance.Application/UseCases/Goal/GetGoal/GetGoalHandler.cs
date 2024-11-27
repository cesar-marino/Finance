using Finance.Application.UseCases.Goal.Commons;

namespace Finance.Application.UseCases.Goal.GetGoal
{
    public class GetGoalHandler : IGetGoalHandler
    {
        public Task<GoalResponse> Handle(GetGoalRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}