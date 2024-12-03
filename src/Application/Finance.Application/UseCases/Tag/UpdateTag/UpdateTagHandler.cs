using Finance.Application.UseCases.Tag.Commons;
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
            var tag = await tagRepository.FindAsync(
                userId: request.UserId,
                entityId: request.TagId,
                cancellationToken);

            tag.ChangeName(request.Name);

            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
