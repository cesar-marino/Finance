using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public class GetTagRequest(Guid tagId) : IRequest<TagResponse>
    {
        public Guid TagId { get; } = tagId;
    }
}
