using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public class EnableTagHandler(ITagRepository tagRepository) : IEnableTagHandler
    {
        public async Task<TagResponse> Handle(EnableTagRequest request, CancellationToken cancellationToken)
        {
            await tagRepository.FindAsync(
                accountId: request.AccountId,
                entityId: request.TagId,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}
