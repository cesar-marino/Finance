using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandler(IGoalRepository goalRepository) : ICreateGoalHandler
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

            throw new NotImplementedException();
        }
    }
}