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
                id: request.TagId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            tag.ChangeName(name: request.Name);

            await tagRepository.UpdateAsync(
                aggregate: tag,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);
            return TagResponse.FromEntity(tag: tag);
        }
    }
}
