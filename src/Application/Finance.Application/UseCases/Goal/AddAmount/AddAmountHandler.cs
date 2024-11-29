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
            var goal = await goalRepository.FindAsync(request.AccountId, request.GoalId, cancellationToken);

            await goalRepository.UpdateAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}