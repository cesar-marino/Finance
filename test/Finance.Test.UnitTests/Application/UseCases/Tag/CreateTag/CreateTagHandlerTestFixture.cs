using Bogus;
using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandlerTestFixture : FixtureBase
    {
        public CreateTagRequest MakeCreateTagRequest() => new(name: Faker.Random.String(5));
    }
}
