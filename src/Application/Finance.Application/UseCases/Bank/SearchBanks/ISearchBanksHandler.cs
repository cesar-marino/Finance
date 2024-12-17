using MediatR;

namespace Finance.Application.UseCases.Bank.SearchBanks
{
    public interface ISearchBanksHandler : IRequestHandler<SearchBanksRequest, SearchBanksResponse>
    {

    }
}