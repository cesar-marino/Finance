
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalHandler(
        IGoalRepository goalRepository,
        IUnitOfWork unitOfWork) : IRemoveGoalHandler
    {
        public async Task Handle(RemoveGoalRequest request, CancellationToken cancellationToken)
        {
            // await goalRepository.FindAsync(request.UserId, request.GoalId, cancellationToken);
            await goalRepository.RemoveAsync(request.UserId, request.GoalId, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}