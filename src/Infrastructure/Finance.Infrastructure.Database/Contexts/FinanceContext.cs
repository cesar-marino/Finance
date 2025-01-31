﻿using Finance.Domain.Exceptions;
using Finance.Domain.SeedWork;
using Finance.Infrastructure.Database.Configurations;
using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Database.Contexts
{
    public class FinanceContext(DbContextOptions<FinanceContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<UserModel> Users { get; private set; }
        public DbSet<TagModel> Tags { get; private set; }
        public DbSet<CategoryModel> Categories { get; private set; }
        public DbSet<LimitModel> Limits { get; private set; }

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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new LimitConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
