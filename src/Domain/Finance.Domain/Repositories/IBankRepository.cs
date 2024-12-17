using Finance.Domain.Entities;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Repositories
{
    public interface IBankRepository : IGeneralRepository<BankEntity>
    {
        Task<SearchResult<BankEntity>> SearchAsync(
            bool? active,
            string? code,
            string? name,
            int? currentPage,
            int? perPage,
            string? orderBy,
            SearchOrder? order);
    }
}