
using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksHandler(IBankRepository bankRepository) : ISearchBanksHandler
    {
        public async Task<SearchBanksResponse> Handle(SearchBanksRequest request, CancellationToken cancellationToken)
        {
            var result = await bankRepository.SearchAsync(
                active: request.Active,
                code: request.Code,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order);

            return new(
                currentPage: result.CurrentPage,
                perPage: result.PerPage,
                total: result.Total,
                orderBy: result.OrderBy,
                order: result.Order,
                items: result.Items.Select(BankResponse.FromEntity).ToList());
        }
    }
}