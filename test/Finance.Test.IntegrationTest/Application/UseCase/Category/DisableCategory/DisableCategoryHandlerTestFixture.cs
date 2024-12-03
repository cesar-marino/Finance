using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.DisableCategory
{
    public class DisableCategoryHandlerTestFixture : FixtureBase
    {
        public DisableCategoryRequest MakeDisableCategoryRequest(
            Guid? userId = null,
            Guid? categoryId = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid());
    }
}