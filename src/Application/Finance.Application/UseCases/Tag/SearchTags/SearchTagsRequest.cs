using Finance.Application.Commons;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsRequest(
        Guid accountId,
        int page = 1,
        int perPage = 50,
        SearchOrder order = SearchOrder.Asc,
        bool? active = null,
        string? name = null) : RequestSearchPagination(page, perPage, order), IRequest<SearchTagsResponse>
    {
        public Guid AccountId { get; } = accountId;
        public bool? Active { get; } = active;
        public string? Name { get; } = name;
    }
}
