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
            var tag = await tagRepository.FindAsync(request.TagId, cancellationToken);
            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
