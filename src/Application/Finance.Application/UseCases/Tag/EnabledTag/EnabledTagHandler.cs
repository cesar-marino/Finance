using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandler : IEnabledTagHandler
    {
        public Task<TagResponse> Handle(EnabledTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
