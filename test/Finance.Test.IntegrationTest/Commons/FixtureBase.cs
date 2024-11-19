using Bogus;
using Finance.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Commons
{
    public abstract class FixtureBase
    {
        public CancellationToken CancellationToken { get; } = CancellationToken.None;
        public Faker Faker { get; } = new("pt_BR");

        public FinanceContext MakeProjectContext() => new(
            new DbContextOptionsBuilder<FinanceContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    }
}