using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public interface IEnableTagHandler : IRequestHandler<EnableTagRequest, TagResponse>
    {
    }
}
