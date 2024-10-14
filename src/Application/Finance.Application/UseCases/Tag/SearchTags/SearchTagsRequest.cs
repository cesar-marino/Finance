using Finance.Application.Commons;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsRequest(
        int page = 1,
        int perPage = 50,
        SearchOrder order = SearchOrder.Asc,
        bool? active = null,
        string? name = null) : RequestSearchPagination(page, perPage, order), IRequest<SearchTagsResponse>
    {
        public bool? Active { get; private set; } = active;
        public string? Name { get; private set; } = name;
    }
}
