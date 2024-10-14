namespace Finance.Domain.SeedWork
{
    public class SearchResponse<TAggregate>(
        int currentPage,
        int perPage,
        int total,
        IReadOnlyList<TAggregate> items) where TAggregate : AggregateRoot
    {
        public int CurrentPage { get; private set; } = currentPage;
        public int PerPage { get; private set; } = perPage;
        public int Total { get; private set; } = total;
        public IReadOnlyList<TAggregate> Items { get; private set; } = items;
    }
}
