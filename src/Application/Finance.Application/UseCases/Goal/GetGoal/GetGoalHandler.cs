using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.GetGoal
{
    public class GetGoalHandler(IGoalRepository goalRepository) : IGetGoalHandler
    {
        public async Task<GoalResponse> Handle(GetGoalRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(
                id: request.GoalId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            return GoalResponse.FromEntity(goal: goal);
        }
    }
}