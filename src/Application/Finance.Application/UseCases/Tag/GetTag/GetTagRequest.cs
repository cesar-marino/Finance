using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public class GetTagRequest(
        Guid userId,
        Guid tagId) : IRequest<TagResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid TagId { get; } = tagId;
    }
}
