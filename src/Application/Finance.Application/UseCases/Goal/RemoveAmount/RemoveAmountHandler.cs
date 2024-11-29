using Finance.Application.UseCases.Goal.Commons;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountHandler : IRemoveAmountHandler
    {
        public Task<GoalResponse> Handle(RemoveAmountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}