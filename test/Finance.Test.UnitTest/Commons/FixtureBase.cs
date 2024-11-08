using Bogus;
using Finance.Domain.Entities;
using Finance.Domain.Enums;
using Finance.Domain.ValueObjects;

namespace Finance.Test.UnitTest.Commons
{
    public abstract class FixtureBase
    {
        public CancellationToken CancellationToken { get; } = CancellationToken.None;
        public Faker Faker { get; } = new("pt_BR");

        public TagEntity MakeTagEntity(
            Guid? tagId = null,
            Guid? accountId = null,
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
            Guid? accountId = null,
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
            limitId: Faker.Random.Guid(),
            accountId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            limitAmount: Faker.Random.Double(),
            createdAt: Faker.Date.Past(),
            updatedAt: Faker.Date.Past());

        public AccountToken MakeAccountToken() => new(
            value: Faker.Random.String(50),
            expiresIn: Faker.Date.Future());

        public AccountEntity MakeAccountEntity(
            bool active = true,
            bool emailConfirmed = false,
            bool phoneConfirmed = false,
            Role role = Role.User) => new(
                accountId: Faker.Random.Guid(),
                active: active,
                username: Faker.Internet.UserName(),
                email: Faker.Internet.Email(),
                emailConfirmed: emailConfirmed,
                phone: Faker.Phone.PhoneNumber(),
                phoneConfirmed: phoneConfirmed,
                password: Faker.Internet.Password(),
                role: role,
                createdAt: Faker.Date.Past(),
                updatedAt: Faker.Date.Past());
    }
}
