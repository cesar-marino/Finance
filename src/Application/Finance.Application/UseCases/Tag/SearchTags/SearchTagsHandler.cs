using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsHandler(ITagRepository tagRepository) : ISearchTagsHandler
    {
        public async Task<SearchTagsResponse> Handle(SearchTagsRequest request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.SearchAsync(
                page: request.Page,
                perPage: request.PerPage,
                active: request.Active,
                name: request.Name,
                order: request.Order,
                cancellationToken);

            return new(
                page: request.Page,
                perPage: request.PerPage,
                total: tags.Total,
                order: request.Order,
                items: tags.Items);
        }
    }
}
