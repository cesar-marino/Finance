using Finance.Application.UseCases.Tag.SearchTags;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;
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

        public SearchResponse<TagEntity> MakeSearchResponse() => new(
                currentPage: Faker.Random.Int(),
                perPage: Faker.Random.Int(),
                total: Faker.Random.Int(),
                items: [MakeTagEntity(), MakeTagEntity(), MakeTagEntity()]);
    }
}
