using Finance.Application.UseCases.Tag.GetTag;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.GetTag
{
    public class GetTagHandlerTestFixture : FixtureBase
    {
        public GetTagRequest MakeGetTagRequest() => new(tagId: Faker.Random.Guid());
    }
}
