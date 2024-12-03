using Finance.Application.UseCases.Goal.Commons;
using MediatR;

namespace Finance.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountRequest(
        Guid goalId,
        Guid userId,
        double amount) : IRequest<GoalResponse>
    {
        public Guid GoalId { get; } = goalId;
        public Guid UserId { get; } = userId;
        public double Amount { get; } = amount;
    }
}