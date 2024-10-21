using Finance.Application.UseCases.Tag.Commons;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandler : ICreateTagHandler
    {
        public Task<TagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
