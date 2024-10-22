using Finance.Application.UseCases.Tag.SerachTags;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsHandlerTestFixture : FixtureBase
    {
        public SearchTagsRequest MakeSearchTagsRequest() => new(
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            order: Domain.SeedWork.SearchOrder.Asc,
            active: Faker.Random.Bool(),
            name: Faker.Random.String(5));

        public SearchResult<TagEntity> MakeTagEntityList() => new(
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            total: Faker.Random.Int(),
            order: SearchOrder.Asc,
            items: []);
    }
}
