using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public class GetTagHandler(ITagRepository tagRepository) : IGetTagHandler
    {
        public async Task<TagResponse> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(request.UserId, request.TagId, cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
