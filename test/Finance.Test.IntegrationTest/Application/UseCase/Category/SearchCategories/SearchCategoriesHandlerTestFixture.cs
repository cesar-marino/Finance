using Finance.Application.UseCases.Category.SearchCategories;
using Finance.Domain.Enums;
using Finance.Domain.SeedWork;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.SearchCategories
{
    public class SearchCategoriesHandlerTestFixture : FixtureBase
    {
        public SearchCategoriesRequest MakeSearchCategoriesRequest(
            bool active = true,
            CategoryType categoryType = CategoryType.Expenditure,
            string? name = null,
            int? currentPage = null,
            int? perPage = null,
            string? orderBy = null,
            SearchOrder order = SearchOrder.Asc) => new(
                active: active,
                categoryType: categoryType,
                name: name ?? Faker.Random.String(5),
                currentPage: currentPage ?? Faker.Random.Int(),
                perPage: perPage ?? Faker.Random.Int(),
                orderBy: orderBy ?? Faker.Random.String(),
                order: order);
    }
}