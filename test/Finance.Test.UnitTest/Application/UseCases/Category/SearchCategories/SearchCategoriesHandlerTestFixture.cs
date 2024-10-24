using Finance.Application.UseCases.Category.SearchCategories;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesHandlerTestFixture : FixtureBase
    {
        public SearchCategoriesRequest MakeSearchCategoriesRequest(bool active = true) => new(
            active: active,
            categoryType: Domain.Enums.CategoryType.Revenue,
            name: Faker.Random.String(5),
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            orderBy: Faker.Random.String(5),
            order: Domain.SeedWork.SearchOrder.Asc);

        public SearchResult<CategoryEntity> MakeSearchResult() => new(
            currentPage: Faker.Random.Int(),
            perPage: Faker.Random.Int(),
            total: Faker.Random.Int(),
            orderBy: Faker.Random.String(5),
            order: SearchOrder.Asc,
            items: []);
    }
}
