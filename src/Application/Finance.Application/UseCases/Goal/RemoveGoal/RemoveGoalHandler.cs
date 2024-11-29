
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalHandler(IGoalRepository goalRepository) : IRemoveGoalHandler
    {
        public async Task Handle(RemoveGoalRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);
            throw new NotImplementedException();
        }
    }
}