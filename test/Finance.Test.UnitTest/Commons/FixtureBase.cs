using Bogus;
using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Test.UnitTest.Commons
{
    public abstract class FixtureBase
    {
        public CancellationToken CancellationToken { get; } = CancellationToken.None;
        public Faker Faker { get; } = new("pt_BR");

        public TagEntity MakeTagEntity(
            Guid? accountId = null,
            Guid? tagId = null,
            bool active = true,
            string? name = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                tagId: tagId ?? Faker.Random.Guid(),
                active: active,
                name: name ?? Faker.Random.String(5),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());

        public CategoryEntity MakeCategoryEntity(
            Guid? accountId = null,
            Guid? categoryId = null,
            bool active = true,
            CategoryType categoryType = CategoryType.Expenditure,
            string? name = null,
            string? icon = null,
            string? color = null,
            Guid? superCategoryId = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid(),
                active: active,
                categoryType: categoryType,
                name: name ?? Faker.Random.String(5),
                icon: icon ?? Faker.Random.String(5),
                color: color ?? Faker.Random.String(5),
                superCategoryId: superCategoryId,
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());

        public LimitEntity MakeLimitEntity() => new(
            accountId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            limitId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            limitAmount: Faker.Random.Double(),
            createdAt: Faker.Date.Past(),
            updatedAt: Faker.Date.Past());
    }
}
