using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagRequest(
        Guid userId,
        Guid tagId,
        string name) : IRequest<TagResponse>
    {
        public Guid UserId { get; } = userId;
        public Guid TagId { get; } = tagId;
        public string Name { get; } = name;
    }
}
