﻿using Finance.Application.UseCases.Limit.Commons;
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
                accountId: request.AccountId,
                entityId: request.LimitId,
                cancellationToken);

            var existAccount = await limitRepository.CheckAccountByIdAsync(request.AccountId, cancellationToken);
            if (!existAccount)
                throw new NotFoundException("Account");

            var existCategory = await limitRepository.CheckCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (!existCategory)
                throw new NotFoundException("Category");

            await limitRepository.UpdateAsync(limit, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
