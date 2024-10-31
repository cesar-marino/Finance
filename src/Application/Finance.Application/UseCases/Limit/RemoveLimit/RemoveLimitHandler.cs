using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitHandler(
        ILimitRepository limitRepository,
        IUnitOfWork unitOfWork) : IRemoveLimitHandler
    {
        public async Task<bool> Handle(RemoveLimitRequest request, CancellationToken cancellationToken)
        {
            await limitRepository.RemoveAsync(
                accountId: request.AccountId,
                limitId: request.LimitId,
                cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
