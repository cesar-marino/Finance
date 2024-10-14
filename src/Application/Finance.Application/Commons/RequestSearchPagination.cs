using Finance.Domain.SeedWork;

namespace Finance.Application.Commons
{
    public abstract class RequestSearchPagination
    {
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public SearchOrder Order { get; private set; }

        protected RequestSearchPagination(
            int page = 1,
            int perPage = 50,
            SearchOrder order = SearchOrder.Asc)
        {
            Page = page;
            PerPage = perPage;
            Order = order;
        }
    }
}
