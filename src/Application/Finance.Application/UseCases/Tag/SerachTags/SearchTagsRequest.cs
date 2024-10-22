using Finance.Application.Commons;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public class SearchTagsRequest(
        int currentPage = 1,
        int perPage = 50,
        SearchOrder order = SearchOrder.Asc,
        bool? active = null,
        string? name = null) : SearchPaginationRequest(currentPage, perPage, order), IRequest<SearchTagsResponse>
    {
        public bool? Active { get; } = active;
        public string? Name { get; } = name;
    }
}
