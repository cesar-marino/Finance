
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksHandler(IBankRepository bankRepository) : ISearchBanksHandler
    {
        public async Task<SearchBanksResponse> Handle(SearchBanksRequest request, CancellationToken cancellationToken)
        {
            _ = await bankRepository.SearchAsync(
                active: request.Active,
                code: request.Code,
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order);

            throw new NotImplementedException();
        }
    }
}