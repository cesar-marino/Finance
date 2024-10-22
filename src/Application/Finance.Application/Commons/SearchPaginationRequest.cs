using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationRequest
    {
        public int? CurrentPage { get; }
        public int? PerPage { get; }
        public string? OrderBy { get; }
        public SearchOrder Order { get; }

        protected SearchPaginationRequest(
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder order)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            OrderBy = orderBy;
            Order = order;
        }
    }
}
