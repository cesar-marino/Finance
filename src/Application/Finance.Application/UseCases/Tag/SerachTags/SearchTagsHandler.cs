using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public class SearchTagsHandler(ITagRepository tagRepository) : ISearchTagsHandler
    {
        public async Task<SearchTagsResponse> Handle(SearchTagsRequest request, CancellationToken cancellationToken)
        {
            await tagRepository.SearchAsync(
                active: request.Active,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                order: request.Order);

            throw new NotImplementedException();
        }
    }
}
