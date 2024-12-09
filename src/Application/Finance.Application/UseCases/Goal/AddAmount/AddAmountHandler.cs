using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.AddAmount
{
    public class AddAmountHandler(
        IGoalRepository goalRepository,
        IUnitOfWork unitOfWork) : IAddAmountHandler
    {
        public async Task<GoalResponse> Handle(AddAmountRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(
                id: request.GoalId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            goal.AddAmount(request.Amount);

            await goalRepository.UpdateAsync(
                aggregate: goal,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return GoalResponse.FromEntity(goal: goal);
        }
    }
}