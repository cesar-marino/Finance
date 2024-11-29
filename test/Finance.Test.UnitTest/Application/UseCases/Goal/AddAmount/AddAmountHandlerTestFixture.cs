using Finance.Application.UseCases.Goal.AddAmount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.AddAmount
{
    public class AddAmountHandlerTestFixture : FixtureBase
    {
        public AddAmountRequest MakeAddAmountRequest() => new(
            goalId: Faker.Random.Guid(),
            accountId: Faker.Random.Guid(),
            amount: Faker.Random.Double());
    }
}