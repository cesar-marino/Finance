using Finance.Application.UseCases.Goal.RemoveGoal;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalHandlerTestFixture : FixtureBase
    {
        public RemoveGoalRequest MakeRemoveGoalRequest() => new(
            goalId: Faker.Random.Guid(),
            userId: Faker.Random.Guid());
    }
}