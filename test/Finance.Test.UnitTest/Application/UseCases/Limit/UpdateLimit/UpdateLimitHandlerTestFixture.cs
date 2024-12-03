using Finance.Application.UseCases.Limit.UpdateLimit;
using Finance.Domain.Entities;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandlerTestFixture : FixtureBase
    {
        public UpdateLimitRequest MakeUpdateLimitRequest(Guid? accountId = null, Guid? limitId = null) => new(
            userId: accountId ?? Faker.Random.Guid(),
            limitId: limitId ?? Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            limitAmount: Faker.Random.Double());
    }
}
