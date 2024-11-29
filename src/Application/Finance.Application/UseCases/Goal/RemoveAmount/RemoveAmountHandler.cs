using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountHandler(IGoalRepository goalRepository) : IRemoveAmountHandler
    {
        public async Task<GoalResponse> Handle(RemoveAmountRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}