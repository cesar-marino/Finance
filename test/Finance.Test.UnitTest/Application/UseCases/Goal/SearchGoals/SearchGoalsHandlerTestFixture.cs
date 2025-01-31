using Finance.Application.UseCases.Goal.SearchGoals;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsHandlerTestFixture : FixtureBase
    {
        public SearchGoalsRequest MakeSearchGoalsRequest() => new(
            name: Faker.Random.String(5),
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            orderBy: Faker.Random.String(5),
            order: SearchOrder.Asc);

        public SearchResult<GoalEntity> MakeSearchGoalsResult() => new(
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            total: Faker.Random.Int(),
            orderBy: Faker.Random.String(5),
            order: SearchOrder.Asc,
            items: [MakeGoalEntity(), MakeGoalEntity(), MakeGoalEntity()]);
    }
}