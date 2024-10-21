using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandlerTestFixture : FixtureBase
    {
        public UpdateTagRequest MakeUpdateTagRequest() => new(
            accountId: Faker.Random.Guid(),
            tagId: Faker.Random.Guid(),
            name: Faker.Random.String(5));
    }
}
