using Finance.Application.UseCases.Category.GetCategory;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandlerTestFixture : FixtureBase
    {
        public GetCategoryRequest MakeGetCategoryRequest() => new(
            accountId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid());
    }
}
