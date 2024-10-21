using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.GetTag
{
    public class GetTagHandler : IGetTagHandler
    {
        public Task<TagResponse> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
