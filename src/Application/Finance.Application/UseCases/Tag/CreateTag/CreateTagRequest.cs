using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public class CreateTagRequest(
        Guid userId,
        string name) : IRequest<TagResponse>
    {
        public Guid UserId { get; } = userId;
        public string Name { get; } = name;
    }
}
