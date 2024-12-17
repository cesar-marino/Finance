using Finance.Application.Commons;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksRequest(
        bool? active,
        string? code,
        string? name,
        int? currentPage,
        int? perPage,
        string? orderBy,
        SearchOrder? order) : SearchPaginationRequest(currentPage, perPage, orderBy, order), IRequest<SearchBanksResponse>
    {
        public bool? Active { get; } = active;
        public string? Code { get; } = code;
        public string? Name { get; } = name;
    }
}