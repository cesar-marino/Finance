using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public class EnableTagRequest(
        Guid accountId,
        Guid tagId) : IRequest<TagResponse>
    {
        public Guid AccountId { get; } = accountId;
        public Guid TagId { get; } = tagId;
    }
}
