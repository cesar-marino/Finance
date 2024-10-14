using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class ResponseSearchPagination<TAggregate> where TAggregate : AggregateRoot
    {
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int Total { get; private set; }
        public SearchOrder Order { get; private set; }
        public IReadOnlyList<TAggregate> Items { get; private set; }

        protected ResponseSearchPagination(
            int page,
            int perPage,
            int total,
            SearchOrder order,
            IReadOnlyList<TAggregate> items)
        {
            Page = page;
            PerPage = perPage;
            Total = total;
            Order = order;
            Items = items;
        }
    }
}
