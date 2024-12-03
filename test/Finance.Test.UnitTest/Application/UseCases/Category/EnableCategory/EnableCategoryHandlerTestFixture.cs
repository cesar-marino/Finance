using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandlerTestFixture : FixtureBase
    {
        public EnableCategoryRequest MakeEnableCategoryRequest() => new(
            userId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid());
    }
}
