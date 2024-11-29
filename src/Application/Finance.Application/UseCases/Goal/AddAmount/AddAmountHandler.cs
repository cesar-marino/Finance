using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.AddAmount
{
    public class AddAmountHandler(IGoalRepository goalRepository) : IAddAmountHandler
    {
        public async Task<GoalResponse> Handle(AddAmountRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);

            await goalRepository.UpdateAsync(goal, cancellationToken);

            throw new NotImplementedException();
        }
    }
}