using Finance.Application.UseCases.Goal.CreateGoal;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandlerTestFixture : FixtureBase
    {
        public CreateGoalRequest MakeCreateGoalRequest() => new(
            accountId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            expectedAmount: Faker.Random.Double(min: 0));
    }
}