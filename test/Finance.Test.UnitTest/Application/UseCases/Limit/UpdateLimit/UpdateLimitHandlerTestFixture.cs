using Finance.Application.UseCases.Limit.UpdateLimit;
using Finance.Domain.Entities;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandlerTestFixture : FixtureBase
    {
        public UpdateLimitRequest MakeUpdateLimitRequest() => new(
            accountId: Faker.Random.Guid(),
            limitId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            limitAmount: Faker.Random.Double());
    }
}
