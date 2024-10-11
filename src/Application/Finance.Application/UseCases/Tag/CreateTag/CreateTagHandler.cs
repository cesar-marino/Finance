﻿using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandler(ITagRepository tagRepository) : ICreateTagHandler
    {
        public async Task<TagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = new TagEntity(name: request.Name);
            await tagRepository.InsertAsync(tag, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
