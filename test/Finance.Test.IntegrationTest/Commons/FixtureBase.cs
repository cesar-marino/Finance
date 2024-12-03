using Bogus;
using Finance.Domain.Enums;
using Finance.Infrastructure.Database.Contexts;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.IntegrationTest.Commons
{
    public abstract class FixtureBase
    {
        public CancellationToken CancellationToken { get; } = CancellationToken.None;
        public Faker Faker { get; } = new("pt_BR");

        public FinanceContext MakeFinanceContext() => new(
            new DbContextOptionsBuilder<FinanceContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        public IConfiguration MakeConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "JWT:SecretKey", "Minha@Super#Secreta&Chave*Privada!2024" },
                { "JWT:AccessTokenValidityInMinutes", "1440" },
                { "JWT:ValidIssuer", "localhost:8080" },
                { "JWT:ValidAudience", "localhost:8080" },
                { "JWT:RefreshTokenValidityInMinutes", "10080" },
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(initialData: myConfiguration!)
                .Build();
        }

        public UserModel MakeUserModel(
            Guid? userId = null,
            bool active = true,
            string? username = null,
            string? email = null,
            bool emailConfirmed = false,
            string? phone = null,
            bool phoneConfirmed = false,
            string? password = null,
            string? accessTokenValue = null,
            DateTime? accessTokenExpiresIn = null,
            string? refreshTokenValue = null,
            DateTime? refreshTokenExpiresIn = null,
            string? role = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                active: active,
                username: username ?? Faker.Internet.UserName(),
                email: email ?? Faker.Internet.Email(),
                emailConfirmed: emailConfirmed,
                phone: phone ?? Faker.Phone.PhoneNumber(),
                phoneConfirmed: phoneConfirmed,
                password: password ?? Faker.Internet.Password(),
                accessTokenValue: accessTokenValue ?? Faker.Random.Guid().ToString(),
                accessTokenExpiresIn: accessTokenExpiresIn ?? Faker.Date.Future(),
                refreshTokenValue: refreshTokenValue ?? Faker.Random.Guid().ToString(),
                refreshTokenExpiresIn: refreshTokenExpiresIn ?? Faker.Date.Future(),
                role: role ?? "User",
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());

        public CategoryModel MakeCategoryModel(
            Guid? userId = null,
            Guid? categoryId = null,
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
                normalizedName: name ?? Faker.Random.String(5),
                icon: icon ?? Faker.Random.String(5),
                color: color ?? Faker.Random.String(5),
                superCategoryId: superCategoryId,
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());
    }
}