using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandlerTestFixture : FixtureBase
    {
        public DisableCategoryRequest MakeDisableCategoryRequest() => new(
            userId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid());
    }
}
