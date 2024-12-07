using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public class GetTagHandler(ITagRepository tagRepository) : IGetTagHandler
    {
        public async Task<TagResponse> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.FindAsync(
                id: request.TagId,
                userId: request.UserId,
                cancellationToken: cancellationToken);

            return TagResponse.FromEntity(tag: tag);
        }
    }
}
