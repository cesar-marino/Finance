using Finance.Application.UseCases.Goal.RemoveAmount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountHandlerTestFixture : FixtureBase
    {
        public RemoveAmountRequest MakeRemoveAmountRequest() => new(
            goalId: Faker.Random.Guid(),
            userId: Faker.Random.Guid(),
            amount: Faker.Random.Double());
    }
}