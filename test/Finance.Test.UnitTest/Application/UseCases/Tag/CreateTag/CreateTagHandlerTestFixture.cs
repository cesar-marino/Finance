using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandlerTestFixture : FixtureBase
    {
        public CreateTagRequest MakeCreateTagRequest() => new(
            userId: Faker.Random.Guid(),
            name: Faker.Random.String(5));
    }
}
