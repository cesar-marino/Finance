using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public class EnableTagHandler(ITagRepository tagRepository) : IEnableTagHandler
    {
        public async Task<TagResponse> Handle(EnableTagRequest request, CancellationToken cancellationToken)
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
