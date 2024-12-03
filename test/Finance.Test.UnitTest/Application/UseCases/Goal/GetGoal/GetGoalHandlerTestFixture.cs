using Finance.Application.UseCases.Goal.GetGoal;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.GetGoal
{
    public class GetGoalHandlerTestFixture : FixtureBase
    {
        public GetGoalRequest MakeGetGoalRequest() => new(
            goalId: Faker.Random.Guid(),
            userId: Faker.Random.Guid());
    }
}