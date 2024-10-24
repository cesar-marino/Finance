using Finance.Domain.Exceptions;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Configurations;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Contexts
{
    public class FinanceContext(DbContextOptions<FinanceContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<TagModel> Tags { get; private set; }
        public DbSet<CategoryModel> Categories { get; private set; }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
