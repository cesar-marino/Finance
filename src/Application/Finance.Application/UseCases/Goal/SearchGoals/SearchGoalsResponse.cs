using Finance.Application.Commons;
using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsResponse(
        int? currentPage,
        int? perPage,
        int total,
        string? orderBy,
        SearchOrder? order,
        IReadOnlyList<GoalResponse> items) : SearchPaginationResponse<GoalResponse>(currentPage, perPage, total, orderBy, order, items)
    {
    }
}