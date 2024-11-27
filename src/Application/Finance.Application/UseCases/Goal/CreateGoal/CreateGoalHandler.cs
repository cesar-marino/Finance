using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandler(IGoalRepository goalRepository) : ICreateGoalHandler
    {
        public async Task<GoalResponse> Handle(CreateGoalRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.CheckAccountAsync(request.AccountId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}