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
            var existUser = await goalRepository.CheckUserAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken);

            if (!existUser)
                throw new NotFoundException("User");

            var goal = new GoalEntity(
                userId: request.UserId,
                name: request.Name,
                expectedAmount: request.ExpectedAmount);

            await goalRepository.InsertAsync(
                aggregate: goal,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return GoalResponse.FromEntity(goal: goal);
        }
    }
}