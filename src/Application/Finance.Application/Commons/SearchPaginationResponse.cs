using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class SearchPaginationResponse<TAggregate> where TAggregate : AggregateRoot
    {
        public int CurrentPage { get; }
        public int PerPage { get; }
        public int Total { get; }
        public SearchOrder Order { get; }
        public IReadOnlyList<TAggregate> Items { get; }

        protected SearchPaginationResponse(
            int currentPage,
            int perPage,
            int total,
            SearchOrder order,
            IReadOnlyList<TAggregate> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            Order = order;
            Items = items;
        }
    }
}
