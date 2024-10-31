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
            var existAccount = await limitRepository.CheckAccountByIdAsync(request.AccountId, cancellationToken);

            if (!existAccount)
                throw new NotFoundException("Account");

            var existCategory = await limitRepository.CheckCategoryByIdAsync(request.CategoryId, cancellationToken);

            if (!existCategory)
                throw new NotFoundException("Category");

            var limit = new LimitEntity(
                accountId: request.AccountId,
                categoryId: request.CategoryId,
                name: request.Name,
                limitAmount: request.LimitAmount);

            await limitRepository.InsertAsync(limit, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return LimitResponse.FromEntity(limit);
        }
    }
}
