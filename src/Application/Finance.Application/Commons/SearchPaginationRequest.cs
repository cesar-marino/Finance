using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationRequest
    {
        public int? CurrentPage { get; set; }
        public int? PerPage { get; set; }
        public string? OrderBy { get; set; }
        public SearchOrder? Order { get; set; }

        protected SearchPaginationRequest(
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            OrderBy = orderBy;
            Order = order;
        }
    }
}
