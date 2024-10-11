using Finance.Application.UseCases.Tags.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tags.CreateTag
{
    public class CreateTagRequest(string name) : IRequest<TagResponse>
    {
        public string Name { get; private set; } = name;
    }
}
