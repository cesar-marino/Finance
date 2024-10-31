using MediatR;

namespace Finance.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitRequest(
        Guid accountId,
        Guid limitId) : IRequest<bool>
    {
        public Guid AccountId { get; } = accountId;
        public Guid LimitId { get; } = limitId;
    }
}