﻿using Finance.Application.UseCases.Limit.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandler(ILimitRepository limitRepository) : ICreateLimitHandler
    {
        public async Task<LimitResponse> Handle(CreateLimitRequest request, CancellationToken cancellationToken)
        {
            _ = await limitRepository.CheckAccountByIdAsync(request.AccountId, cancellationToken);

            var limit = new LimitEntity(accountId: request.AccountId, categoryId: request.CategoryId, name: request.Name, limitAmount: request.LimitAmount);
            await limitRepository.InsertAsync(limit, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
