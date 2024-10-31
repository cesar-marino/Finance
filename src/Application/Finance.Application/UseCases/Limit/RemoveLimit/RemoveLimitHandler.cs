using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitHandler(ILimitRepository limitRepository) : IRemoveLimitHandler
    {
        public async Task<bool> Handle(RemoveLimitRequest request, CancellationToken cancellationToken)
        {
            await limitRepository.RemoveAsync(
                accountId: request.AccountId,
                limitId: request.LimitId,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
