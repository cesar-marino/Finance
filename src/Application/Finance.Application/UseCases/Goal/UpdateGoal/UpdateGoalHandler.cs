using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandler(IGoalRepository goalRepository) : IUpdateGoalHandler
    {
        public async Task<GoalResponse> Handle(UpdateGoalRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);

            await goalRepository.UpdateAsync(goal, cancellationToken);

            throw new NotImplementedException();
        }
    }
}