using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagRequest(
        Guid accountId,
        Guid tagId,
        string name) : IRequest<TagResponse>
    {
        public Guid AccountId { get; } = accountId;
        public Guid TagId { get; } = tagId;
        public string Name { get; } = name;
    }
}
