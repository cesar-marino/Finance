using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagRequest(Guid tagId) : IRequest<TagResponse>
    {
        public Guid TagId { get; private set; } = tagId;
    }
}
