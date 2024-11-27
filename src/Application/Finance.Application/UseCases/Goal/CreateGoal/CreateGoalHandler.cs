using Finance.Application.UseCases.Goal.Commons;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandler : ICreateGoalHandler
    {
        public Task<GoalResponse> Handle(CreateGoalRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}