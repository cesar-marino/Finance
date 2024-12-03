using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Domain.Enums;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.CreateCategory
{
    public class CreateCategoryHandlerTestFixture : FixtureBase
    {
        public CreateCategoryRequest MakeCreateCategoryRequest(
            Guid? userId = null,
            CategoryType? categoryType = null,
            string? name = null,
            string? icon = null,
            string? color = null,
            Guid? superCategoryId = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                categoryType: categoryType ?? CategoryType.Expenditure,
                name: name ?? Faker.Random.String(5),
                icon: icon ?? Faker.Random.String(5),
                color: color ?? Faker.Random.String(5),
                superCategoryId: superCategoryId ?? null);
    }
}