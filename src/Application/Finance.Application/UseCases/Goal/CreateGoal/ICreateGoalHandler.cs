using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.CreateGoal
{
    public interface ICreateGoalHandler : IRequestHandler<CreateGoalRequest, GoalResponse>
    {

    }
}