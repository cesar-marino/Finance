using Finance.Application.UseCases.Tag.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork) : ICreateTagHandler
    {
        public async Task<TagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = new TagEntity(userId: request.UserId, name: request.Name);

            await tagRepository.InsertAsync(tag, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return TagResponse.FromEntity(tag);
        }
    }
}
