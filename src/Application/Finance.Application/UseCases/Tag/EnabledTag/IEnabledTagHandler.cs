using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.EnabledTag
{
    public interface IEnabledTagHandler : IRequestHandler<EnabledTagRequest, TagResponse>
    {
    }
}
