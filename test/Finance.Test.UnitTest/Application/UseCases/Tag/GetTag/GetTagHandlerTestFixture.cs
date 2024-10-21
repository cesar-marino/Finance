using Finance.Application.UseCases.Tag.GetTag;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.GetTag
{
    public class GetTagHandlerTestFixture : FixtureBase
    {
        public GetTagRequest MakeGetTagRequest() => new(
            accountId: Faker.Random.Guid(),
            tagId: Faker.Random.Guid());
    }
}
