using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public class CreateTagRequest(
        Guid accountId,
        string name) : IRequest<TagResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string Name { get; } = name;
    }
}
