using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountRequest(
        Guid goalId,
        Guid accountId,
        double amount) : IRequest<GoalResponse>
    {
        public Guid GoalId { get; } = goalId;
        public Guid AccountId { get; } = accountId;
        public double Amount { get; } = amount;
    }
}