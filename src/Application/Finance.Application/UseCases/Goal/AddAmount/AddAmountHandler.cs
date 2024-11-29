using Finance.Application.UseCases.Goal.Commons;

namespace Finance.Application.UseCases.Goal.AddAmount
{
    public class AddAmountHandler : IAddAmountHandler
    {
        public Task<GoalResponse> Handle(AddAmountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}