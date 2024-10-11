using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.DisableTag
{
    public interface IDisableTagHandler : IRequestHandler<DisableTagRequest, TagResponse>
    {
    }
}
