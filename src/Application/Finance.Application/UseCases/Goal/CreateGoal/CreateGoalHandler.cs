using Finance.Application.UseCases.Goal.Commons;
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

            throw new NotImplementedException();
        }
    }
}