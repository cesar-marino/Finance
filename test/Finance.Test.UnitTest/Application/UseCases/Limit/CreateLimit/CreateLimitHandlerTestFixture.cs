using Finance.Application.UseCases.Limit.CreateLimit;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandlerTestFixture : FixtureBase
    {
        public CreateLimitRequest MakeCreateLimitRequest() => new(
            userId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            currentAmount: Faker.Random.Double(),
            limitAmount: Faker.Random.Double());
    }
}
