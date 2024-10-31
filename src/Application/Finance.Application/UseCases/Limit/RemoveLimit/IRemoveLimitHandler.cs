using MediatR;

namespace Finance.Application.UseCases.Limit.RemoveLimit
{
    public interface IRemoveLimitHandler : IRequestHandler<RemoveLimitRequest, bool>
    {
    }
}
