using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountHandler(
        IGoalRepository goalRepository,
        IUnitOfWork unitOfWork) : IRemoveAmountHandler
    {
        public async Task<GoalResponse> Handle(RemoveAmountRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(
                id: request.GoalId,
                userId: request.UserId,
                cancellationToken);

            goal.RemoveAmount(request.Amount);

            await goalRepository.UpdateAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return GoalResponse.FromEntity(goal);
        }
    }
}