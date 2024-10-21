using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandler(ITagRepository tagRepository) : IUpdateTagHandler
    {
        public async Task<TagResponse> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.TagId,
                cancellationToken);

            await tagRepository.UpdateAsync(tag, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
