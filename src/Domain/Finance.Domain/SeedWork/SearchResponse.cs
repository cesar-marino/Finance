namespace Finance.Domain.SeedWork
{
    public abstract class SearchResponse<TAggregate> where TAggregate : AggregateRoot
    {
        public int CurrentPage { get; private set; }
        public int PerPage { get; private set; }
        public int Total { get; private set; }
        public IReadOnlyList<TAggregate> Items { get; private set; }

        protected SearchResponse(
            int currentPage,
            int perPage,
            int total,
            IReadOnlyList<TAggregate> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}
