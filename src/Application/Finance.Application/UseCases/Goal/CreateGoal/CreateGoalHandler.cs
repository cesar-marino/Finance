using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandler(
        IGoalRepository goalRepository,
        IUnitOfWork unitOfWork) : ICreateGoalHandler
    {
        public async Task<GoalResponse> Handle(CreateGoalRequest request, CancellationToken cancellationToken)
        {
            var existAccount = await goalRepository.CheckAccountAsync(request.AccountId, cancellationToken);

            if (!existAccount)
                throw new NotFoundException("Account");

            var goal = new GoalEntity(
                accountId: request.AccountId,
                name: request.Name,
                expectedAmount: request.ExpectedAmount);

            await goalRepository.InsertAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}