using Finance.Application.UseCases.Category.SearchCategories;
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
    }
}
