﻿using Finance.Application.UseCases.Limit.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandler(ILimitRepository limitRepository) : IUpdateLimitHandler
    {
        public async Task<LimitResponse> Handle(UpdateLimitRequest request, CancellationToken cancellationToken)
        {
            _ = await limitRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.LimitId,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
