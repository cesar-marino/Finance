using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(x => new { x.AccountId, x.CategoryId });

            builder.Property(x => x.CategoryId)
                .HasColumnName("category_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.AccountId)
                .HasColumnName("account_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithMany(a => a.Categories)
                .HasForeignKey(x => x.AccountId);

            builder.Property(x => x.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.Property(x => x.CategoryType)
                .HasColumnName("category_type");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Icon)
                .HasColumnName("name");

            builder.Property(x => x.Color)
                .HasColumnName("color");

            builder.Property(x => x.SuperCategoryId)
                .HasColumnName("super_category_id");

            builder.HasOne(x => x.SuperCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => new { x.AccountId, x.SuperCategoryId });

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
