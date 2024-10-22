using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public class SearchTagsHandler(ITagRepository tagRepository) : ISearchTagsHandler
    {
        public async Task<SearchTagsResponse> Handle(SearchTagsRequest request, CancellationToken cancellationToken)
        {
            var result = await tagRepository.SearchAsync(
                active: request.Active,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order,
                cancellationToken);

            return new(
                currentPage: result.CurrentPage,
                perPage: result.PerPage,
                total: result.Total,
                orderBy: result.OrderBy,
                order: result.Order,
                items: result.Items);
        }
    }
}
