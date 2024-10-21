using Finance.Application.UseCases.Tag.Commons;
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
                accountId: request.AccountId,
                entityId: request.TagId,
                cancellationToken);

            tag.Enable();

            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
