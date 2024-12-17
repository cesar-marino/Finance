using Finance.Application.Commons;
using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksResponse(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<BankResponse> items) : SearchPaginationResponse<BankResponse>(currentPage, perPage, total, orderBy, order, items)
    {
    }
}