using Finance.Application.UseCases.Tag.SearchTags;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsHandlerTestFixture : FixtureBase
    {
        public SearchTagsRequest MakeSearchTagsRequest() => new(
                page: Faker.Random.Int(),
                perPage: Faker.Random.Int(),
                order: Domain.SeedWork.SearchOrder.Asc,
                active: Faker.Random.Bool(),
                name: Faker.Random.String(5));
    }
}
