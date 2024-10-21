using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandler : IDisableTagHandler
    {
        public Task<TagResponse> Handle(DisableTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
