using Finance.Application.UseCases.Limit.Commons;
using MediatR;

namespace Finance.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitRequest(
        Guid accountId,
        Guid categoryId,
        string name,
        double currentAmount,
        double limitAmount) : IRequest<LimitResponse>
    {
        public Guid AccountId { get; } = accountId;
        public Guid CategoryId { get; } = categoryId;
        public string Name { get; } = name;
        public double CurrentAmount { get; } = currentAmount;
        public double LimitAmount { get; } = limitAmount;
    }
}
