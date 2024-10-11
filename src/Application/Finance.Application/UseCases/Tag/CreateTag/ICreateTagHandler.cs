using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public interface ICreateTagHandler : IRequestHandler<CreateTagRequest, TagResponse>
    {
    }
}
