using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandlerTestFixture : FixtureBase
    {
        public UpdateTagRequest MakeUpdateTagRequest() => new(
            tagId: Faker.Random.Guid(),
            name: Faker.Random.String(5));
    }
}
