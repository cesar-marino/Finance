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
            var goal = await goalRepository.FindAsync(request.UserId, request.GoalId, cancellationToken);
            goal.AddAmount(request.Amount);

            await goalRepository.UpdateAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return GoalResponse.FromEntity(goal);
        }
    }
}