using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public interface IUpdateTagHandler : IRequestHandler<UpdateTagRequest, TagResponse>
    {
    }
}
