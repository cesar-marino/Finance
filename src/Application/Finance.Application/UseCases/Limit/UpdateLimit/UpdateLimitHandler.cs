using Finance.Application.UseCases.Limit.Commons;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandler(
        ILimitRepository limitRepository,
        IUnitOfWork unitOfWork) : IUpdateLimitHandler
    {
        public async Task<LimitResponse> Handle(UpdateLimitRequest request, CancellationToken cancellationToken)
        {
            var limit = await limitRepository.FindAsync(
                userId: request.UserId,
                entityId: request.LimitId,
                cancellationToken);

            var existUser = await limitRepository.CheckUserByIdAsync(request.UserId, cancellationToken);
            if (!existUser)
                throw new NotFoundException("User");

            var existCategory = await limitRepository.CheckCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (!existCategory)
                throw new NotFoundException("Category");

            limit.Update(
                name: request.Name,
                limitAmount: request.LimitAmount,
                categoryId: request.CategoryId);

            await limitRepository.UpdateAsync(limit, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return LimitResponse.FromEntity(limit);
        }
    }
}
