using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandler : IUpdateTagHandler
    {
        public Task<TagResponse> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
