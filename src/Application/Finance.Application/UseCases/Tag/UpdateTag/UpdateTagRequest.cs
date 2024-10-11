using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagRequest(
        Guid tagId,
        string name) : IRequest<TagResponse>
    {
        public Guid TagId { get; private set; } = tagId;
        public string Name { get; private set; } = name;
    }
}
