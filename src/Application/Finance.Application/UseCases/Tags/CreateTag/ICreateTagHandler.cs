using Finance.Application.UseCases.Tags.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tags.CreateTag
{
    public interface ICreateTagHandler : IRequestHandler<CreateTagRequest, TagResponse>
    {
    }
}
