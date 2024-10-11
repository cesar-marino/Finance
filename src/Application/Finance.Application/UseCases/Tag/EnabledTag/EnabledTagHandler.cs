using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork) : IEnabledTagHandler
    {
        public async Task<TagResponse> Handle(EnabledTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(request.TagId, cancellationToken);
            tag.Enabled();

            await tagRepository.UpdateAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
