using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandlerTestFixture : FixtureBase
    {
        public DisableTagRequest MakeDisableTagRequest() => new(tagId: Faker.Random.Guid());
    }
}
