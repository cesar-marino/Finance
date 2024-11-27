using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.GetGoal
{
    public interface IGetGoalHandler : IRequestHandler<GetGoalRequest, GoalResponse>
    {

    }
}