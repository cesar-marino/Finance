using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationRequest(
        int? currentPage,
        int? perPage,
        string? orderBy,
        SearchOrder? order)
    {
        public int? CurrentPage { get; set; } = currentPage;
        public int? PerPage { get; set; } = perPage;
        public string? OrderBy { get; set; } = orderBy;
        public SearchOrder? Order { get; set; } = order;
    }
}
