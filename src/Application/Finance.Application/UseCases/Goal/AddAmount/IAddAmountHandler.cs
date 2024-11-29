using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.AddAmount
{
    public interface IAddAmountHandler : IRequestHandler<AddAmountRequest, GoalResponse>
    {

    }
}