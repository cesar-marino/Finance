
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsHandler(IGoalRepository goalRepository) : ISearchGoalsHandler
    {
        public async Task<SearchGoalsResponse> Handle(SearchGoalsRequest request, CancellationToken cancellationToken)
        {
            await goalRepository.SearchAsync(
                name: request.Name,
                currentPage: request.CurrentPage,
                perPage: request.PerPage,
                orderBy: request.OrderBy,
                order: request.Order,
                cancellationToken);

            throw new NotImplementedException();
        }
    }
}