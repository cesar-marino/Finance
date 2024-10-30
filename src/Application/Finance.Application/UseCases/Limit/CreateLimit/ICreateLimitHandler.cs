using Finance.Application.UseCases.Limit.Commons;
using MediatR;

namespace Finance.Application.UseCases.Limit.CreateLimit
{
    public interface ICreateLimitHandler : IRequestHandler<CreateLimitRequest, LimitResponse>
    {
    }
}
