using MediatR;

namespace Finance.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitRequest(
        Guid userId,
        Guid limitId) : IRequest<bool>
    {
        public Guid UserId { get; } = userId;
        public Guid LimitId { get; } = limitId;
    }
}