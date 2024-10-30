using Finance.Application.UseCases.Limit.Commons;

namespace Finance.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandler : ICreateLimitHandler
    {
        public Task<LimitResponse> Handle(CreateLimitRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
