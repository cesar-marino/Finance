using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork) : IDisableTagHandler
    {
        public async Task<TagResponse> Handle(DisableTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(
                userId: request.UserId,
                entityId: request.TagId,
                cancellationToken);

            tag.Disable();

            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
