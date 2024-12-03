using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandler(
        IGoalRepository goalRepository,
        IUnitOfWork unitOfWork) : IUpdateGoalHandler
    {
        public async Task<GoalResponse> Handle(UpdateGoalRequest request, CancellationToken cancellationToken)
        {
            var goal = await goalRepository.FindAsync(request.UserId, request.GoalId, cancellationToken);
            goal.Updated(name: request.Name, expectedAmount: request.ExpectedAmount);

            await goalRepository.UpdateAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return GoalResponse.FromEntity(goal);
        }
    }
}