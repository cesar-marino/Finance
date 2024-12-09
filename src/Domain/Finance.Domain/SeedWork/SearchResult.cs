namespace Finance.Domain.SeedWork
{
    public class SearchResult<TAggregate>(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<TAggregate> items) where TAggregate : EntityBase
    {
        public int? CurrentPage { get; } = currentPage;
        public int? PerPage { get; } = perPage;
        public int Total { get; } = total;
        public string? OrderBy { get; } = orderBy;
        public SearchOrder? Order { get; } = order;
        public IReadOnlyList<TAggregate> Items { get; } = items;
    }
}
