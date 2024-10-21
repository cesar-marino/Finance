using Finance.Application.UseCases.Tag.Commons;
using MediatR;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public interface IGetTagHandler : IRequestHandler<GetTagRequest, TagResponse>
    {
    }
}
