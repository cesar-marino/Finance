using Finance.Application.UseCases.Tags.Commons;

namespace Finance.Application.UseCases.Tags.CreateTag
{
    public class CreateTagHandler : ICreateTagHandler
    {
        public Task<TagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
