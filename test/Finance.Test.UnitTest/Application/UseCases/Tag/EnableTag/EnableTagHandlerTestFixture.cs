using Finance.Application.UseCases.Tag.EnableTag;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.EnableTag
{
    public class EnableTagHandlerTestFixture : FixtureBase
    {
        public EnableTagRequest MakeEnableTagRequest() => new(
            userId: Faker.Random.Guid(),
            tagId: Faker.Random.Guid());
    }
}
