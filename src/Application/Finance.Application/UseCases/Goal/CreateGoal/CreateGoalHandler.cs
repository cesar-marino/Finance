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
            var existUser = await goalRepository.CheckUserAsync(request.UserId, cancellationToken);

            if (!existUser)
                throw new NotFoundException("User");

            var goal = new GoalEntity(
                userId: request.UserId,
                name: request.Name,
                expectedAmount: request.ExpectedAmount);

            await goalRepository.InsertAsync(goal, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return GoalResponse.FromEntity(goal);
        }
    }
}