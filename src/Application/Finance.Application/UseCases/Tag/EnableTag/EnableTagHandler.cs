﻿using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public class EnableTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork) : IEnableTagHandler
    {
        public async Task<TagResponse> Handle(EnableTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(
                id: request.TagId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            tag.Enable();

            await tagRepository.UpdateAsync(
                aggregate: tag,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return TagResponse.FromEntity(tag: tag);
        }
    }
}
