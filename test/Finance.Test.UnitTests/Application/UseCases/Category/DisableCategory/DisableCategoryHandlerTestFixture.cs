using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandlerTestFixture : FixtureBase
    {
        public DisableCategoryRequest MakeDisableCategoryRequest() => new(categoryId: Faker.Random.Guid());
    }
}
