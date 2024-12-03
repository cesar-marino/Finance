using Finance.Application.UseCases.Goal.UpdateGoal;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandlerTestFixture : FixtureBase
    {
        public UpdateGoalRequest MakeUpdateGoalRequest() => new(
            goalId: Faker.Random.Guid(),
            userId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            expectedAmount: Faker.Random.Double());
    }
}