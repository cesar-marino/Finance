using Finance.Application.UseCases.Limit.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandler(
        ILimitRepository limitRepository,
        IUnitOfWork unitOfWork) : ICreateLimitHandler
    {
        public async Task<LimitResponse> Handle(CreateLimitRequest request, CancellationToken cancellationToken)
        {
            var existUser = await limitRepository.CheckUserByIdAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken);

            if (!existUser)
                throw new NotFoundException("User");

            var existCategory = await limitRepository.CheckCategoryByIdAsync(
                categoryId: request.CategoryId,
                cancellationToken: cancellationToken);

            if (!existCategory)
                throw new NotFoundException("Category");

            var limit = new LimitEntity(
                userId: request.UserId,
                categoryId: request.CategoryId,
                name: request.Name,
                limitAmount: request.LimitAmount);

            await limitRepository.InsertAsync(
                aggregate: limit,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return LimitResponse.FromEntity(limit: limit);
        }
    }
}
