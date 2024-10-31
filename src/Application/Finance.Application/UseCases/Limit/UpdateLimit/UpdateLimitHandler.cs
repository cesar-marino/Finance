using Finance.Application.UseCases.Limit.Commons;

namespace Finance.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandler : IUpdateLimitHandler
    {
        public Task<LimitResponse> Handle(UpdateLimitRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
