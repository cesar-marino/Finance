using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public interface IRemoveAmountHandler : IRequestHandler<RemoveAmountRequest, GoalResponse>
    {

    }
}