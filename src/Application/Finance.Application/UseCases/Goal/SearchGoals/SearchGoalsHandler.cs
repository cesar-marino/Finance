
using Finance.Application.UseCases.Goal.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsHandler(IGoalRepository goalRepository) : ISearchGoalsHandler
    {
        public async Task<SearchGoalsResponse> Handle(SearchGoalsRequest request, CancellationToken cancellationToken)
        {
            var result = await goalRepository.SearchAsync(
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order,
                cancellationToken);

            return new(
                currentPage: result.CurrentPage,
                perPage: result.PerPage,
                total: result.Total,
                orderBy: result.OrderBy,
                order: result.Order,
                items: result.Items.Select(GoalResponse.FromEntity).ToList());
        }
    }
}