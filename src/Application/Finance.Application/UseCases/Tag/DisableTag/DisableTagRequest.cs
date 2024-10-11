using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.DisableTag
{
    public class DisableTagRequest(Guid tagId) : IRequest<TagResponse>
    {
        public Guid TagId { get; private set; } = tagId;
    }
}
