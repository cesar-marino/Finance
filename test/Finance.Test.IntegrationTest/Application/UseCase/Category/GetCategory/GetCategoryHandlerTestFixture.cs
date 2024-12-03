using Finance.Application.UseCases.Category.GetCategory;
using Finance.Infrastructure.Database.Models;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.GetCategory
{
    public class GetCategoryHandlerTestFixture : FixtureBase
    {
        public GetCategoryRequest MakeGetCategoryRequest(
            Guid? userId = null,
            Guid? categoryId = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid());

        public List<CategoryModel> MakeCategoryList(Guid superCategoryId) => [
                MakeCategoryModel(superCategoryId: superCategoryId),
                MakeCategoryModel(superCategoryId: superCategoryId),
                MakeCategoryModel(superCategoryId: superCategoryId),
                MakeCategoryModel(superCategoryId: superCategoryId),
            ];
    }
}