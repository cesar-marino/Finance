using Finance.Application.Commons;
using Finance.Domain.SeedWork;
using MediatR;

namespace Finance.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsRequest(
        string? name,
        int? currentPage,
        int? perPage,
        string? orderBy,
        SearchOrder? order) : SearchPaginationRequest(currentPage, perPage, orderBy, order), IRequest<SearchGoalsResponse>
    {
        public string? Name { get; } = name;
    }
}