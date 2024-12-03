using MediatR;

namespace Finance.Application.UseCases.Goal.SearchGoals
{
    public interface ISearchGoalsHandler : IRequestHandler<SearchGoalsRequest, SearchGoalsResponse>
    {

    }
}