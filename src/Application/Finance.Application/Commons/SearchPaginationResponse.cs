using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationResponse<TResponse>(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<TResponse> items)
    {
        public int? CurrentPage { get; } = currentPage;
        public int? PerPage { get; } = perPage;
        public int Total { get; } = total;
        public string? OrderBy { get; } = orderBy;
        public SearchOrder? Order { get; } = order;
        public IReadOnlyList<TResponse> Items { get; } = items;
    }
}
