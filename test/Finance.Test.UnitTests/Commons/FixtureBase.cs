using Bogus;
using Finance.Domain.Entities;

namespace Finance.Test.UnitTests.Commons
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
            Guid? categoryId = null,
            bool active = true,
            string? name = null,
            string? icon = null,
            string? color = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                categoryId: categoryId ?? Faker.Random.Guid(),
                active: active,
                name: name ?? Faker.Random.String(5),
                icon: icon ?? Faker.Random.String(5),
                color: color ?? Faker.Random.String(5),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());
    }
}
