using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Configurations
{
    public class LimitConfiguration : IEntityTypeConfiguration<LimitModel>
    {
        public void Configure(EntityTypeBuilder<LimitModel> builder)
        {
            builder.ToTable("limits");
            builder.HasKey(x => new { x.UserId, x.LimitId });

            builder.Property(x => x.LimitId)
                .HasColumnName("limit_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(a => a.Limits)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.CategoryId)
                .HasColumnName("category_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(a => a.Limits)
                .HasForeignKey(x => new { x.UserId, x.CategoryId });

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.LimitAmount)
                .HasColumnName("limit_amount")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            //builder.Ignore(p => p.Events);
        }
    }
}
