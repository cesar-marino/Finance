using Finance.Application.UseCases.Tag.EnabledTag;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandlerTestFixture : FixtureBase
    {
        public EnabledTagRequest MakeEnableTagRequest() => new(tagId: Faker.Random.Guid());
    }
}
