using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<TagModel>
    {
        public void Configure(EntityTypeBuilder<TagModel> builder)
        {
            builder.ToTable("tags");
            builder.HasKey(x => new { x.AccountId, x.TagId });

            builder.Property(x => x.TagId)
                .HasColumnName("tag_id")
                .IsRequired();

            builder.Property(x => x.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithMany(a => a.Tags)
                .HasForeignKey(x => x.AccountId);

            builder.Property(x => x.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp without time zone");

            //builder.Ignore(p => p.Events);
        }
    }
}
