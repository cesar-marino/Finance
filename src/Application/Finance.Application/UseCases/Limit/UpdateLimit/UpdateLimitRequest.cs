using Finance.Application.UseCases.Limit.Commons;
using MediatR;

namespace Finance.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitRequest(
        Guid userId,
        Guid limitId,
        Guid categoryId,
        string name,
        double limitAmount) : IRequest<LimitResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid LimitId { get; } = limitId;
        public Guid CategoryId { get; } = categoryId;
        public string Name { get; } = name;
        public double LimitAmount { get; } = limitAmount;
    }
}
