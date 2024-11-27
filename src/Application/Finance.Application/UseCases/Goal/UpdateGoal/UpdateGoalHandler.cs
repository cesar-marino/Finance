using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandler(IGoalRepository goalRepository) : IUpdateGoalHandler
    {
        public async Task<GoalResponse> Handle(UpdateGoalRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}