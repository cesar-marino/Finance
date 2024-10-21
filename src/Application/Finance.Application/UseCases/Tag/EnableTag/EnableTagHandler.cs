using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.EnableTag
{
    public class EnableTagHandler : IEnableTagHandler
    {
        public Task<TagResponse> Handle(EnableTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
