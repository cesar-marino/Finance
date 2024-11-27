using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.GetGoal
{
    public class GetGoalHandler(IGoalRepository goalRepository) : IGetGoalHandler
    {
        public async Task<GoalResponse> Handle(GetGoalRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);
            throw new NotImplementedException();
        }
    }
}