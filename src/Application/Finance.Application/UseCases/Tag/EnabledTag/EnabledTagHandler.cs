using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandler(ITagRepository tagRepository) : IEnabledTagHandler
    {
        public async Task<TagResponse> Handle(EnabledTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(request.TagId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
