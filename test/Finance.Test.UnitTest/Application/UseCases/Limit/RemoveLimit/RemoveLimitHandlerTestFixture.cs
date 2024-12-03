using Finance.Application.UseCases.Limit.RemoveLimit;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitHandlerTestFixture : FixtureBase
    {
        public RemoveLimitRequest MakeRemoveLimitRequest() => new(
            userId: Faker.Random.Guid(),
            limitId: Faker.Random.Guid());
    }
}
