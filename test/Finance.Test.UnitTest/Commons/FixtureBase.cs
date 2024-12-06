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
            Guid? userId = null,
            bool active = true,
            string? name = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                tagId: tagId ?? Faker.Random.Guid(),
                active: active,
                name: name ?? Faker.Random.String(5),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());

        public CategoryEntity MakeCategoryEntity(
            Guid? categoryId = null,
            Guid? userId = null,
            bool active = true,
            CategoryType categoryType = CategoryType.Expenditure,
            string? name = null,
            string? icon = null,
            string? color = null,
            Guid? superCategoryId = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                userId: userId ?? Faker.Random.Guid(),
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
            userId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            limitAmount: Faker.Random.Double(),
            createdAt: Faker.Date.Past(),
            updatedAt: Faker.Date.Past());

        public UserToken MakeUserToken(
            string? value = null,
            DateTime? expiresIn = null) => new(
                value: value ?? Faker.Random.String(50),
                expiresIn: expiresIn ?? Faker.Date.Future());

        public UserEntity MakeUserEntity(
            bool active = true,
            bool emailConfirmed = false,
            bool phoneConfirmed = false,
            Roles role = Roles.User,
            UserToken? refreshToken = null) => new(
                userId: Faker.Random.Guid(),
                active: active,
                username: Faker.Internet.UserName(),
                email: Faker.Internet.Email(),
                emailConfirmed: emailConfirmed,
                phone: Faker.Phone.PhoneNumber(),
                phoneConfirmed: phoneConfirmed,
                password: Faker.Internet.Password(),
                role: role,
                accessToken: new(Faker.Random.Guid().ToString(), Faker.Date.Future()),
                refreshToken: refreshToken ?? new(Faker.Random.Guid().ToString(), Faker.Date.Future()),
                createdAt: Faker.Date.Past(),
                updatedAt: Faker.Date.Past());

        public GoalEntity MakeGoalEntity(
            Guid? userId = null,
            Guid? goalId = null,
            string? name = null,
            double? expectedAmount = null,
            double? currentAmount = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                goalId: goalId ?? Faker.Random.Guid(),
                name: name ?? Faker.Random.String(5),
                expectedAmount: expectedAmount ?? Faker.Random.Double(),
                currentAmount: currentAmount ?? Faker.Random.Double(),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());

        public BankEntity MakeBankEntity() => new(
            bankId: Faker.Random.Guid(),
            active: Faker.Random.Bool(),
            code: Faker.Random.String(5),
            name: Faker.Random.String(5),
            color: Faker.Random.String(5),
            logo: Faker.Internet.Url(),
            createdAt: Faker.Date.Past(),
            updatedAt: Faker.Date.Past());
    }
}
