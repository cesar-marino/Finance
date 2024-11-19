using Bogus;
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

        public AccountModel MakeAccountModel(
            Guid? accountId = null,
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
                accountId: accountId ?? Faker.Random.Guid(),
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
                role: role ?? Faker.Random.String(),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());
    }
}