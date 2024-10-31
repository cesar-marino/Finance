using Finance.Application.UseCases.Limit.Commons;
using MediatR;

namespace Finance.Application.UseCases.Limit.UpdateLimit
{
    public interface IUpdateLimitHandler : IRequestHandler<UpdateLimitRequest, LimitResponse>
    {
    }
}
