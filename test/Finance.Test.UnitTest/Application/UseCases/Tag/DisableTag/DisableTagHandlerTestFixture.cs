using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandlerTestFixture : FixtureBase
    {
        public DisableTagRequest MakeDisableTagRequest() => new(
            accountId: Faker.Random.Guid(),
            tagId: Faker.Random.Guid());
    }
}
