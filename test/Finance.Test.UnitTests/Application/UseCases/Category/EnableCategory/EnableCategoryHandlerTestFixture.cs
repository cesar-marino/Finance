using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandlerTestFixture : FixtureBase
    {
        public EnableCategoryRequest MakeEnableCategoryRequest() => new(categoryId: Faker.Random.Guid());
    }
}
