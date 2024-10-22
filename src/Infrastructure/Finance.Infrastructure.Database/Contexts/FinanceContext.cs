using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Configurations;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Contexts
{
    public class FinanceContext(DbContextOptions<FinanceContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<TagModel> Tags { get; private set; }

        public async Task CommitAsync(CancellationToken cancellationToken = default) => await SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
