﻿using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork) : IUpdateTagHandler
    {
        public async Task<TagResponse> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(request.TagId, cancellationToken);
            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
