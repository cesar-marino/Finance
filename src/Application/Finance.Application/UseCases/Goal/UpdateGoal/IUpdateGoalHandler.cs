using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.UpdateGoal
{
    public interface IUpdateGoalHandler : IRequestHandler<UpdateGoalRequest, GoalResponse>
    {

    }
}