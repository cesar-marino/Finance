using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandler(ITagRepository tagRepository) : IDisableTagHandler
    {
        public async Task<TagResponse> Handle(DisableTagRequest request, CancellationToken cancellationToken)
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
