using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationResponse<TResponse>
    {
        public int? CurrentPage { get; }
        public int? PerPage { get; }
        public int Total { get; }
        public string? OrderBy { get; }
        public SearchOrder? Order { get; }
        public IReadOnlyList<TResponse> Items { get; }

        protected SearchPaginationResponse(
            int? currentPage,
            int? perPage,
            int total,
            string? orderBy,
            SearchOrder? order,
            IReadOnlyList<TResponse> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            OrderBy = orderBy;
            Order = order;
            Items = items;
        }
    }
}
