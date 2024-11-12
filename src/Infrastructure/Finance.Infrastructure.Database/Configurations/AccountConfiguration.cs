using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("accounts");
            builder.HasKey(x => x.AccountId);

            builder.Property(x => x.AccountId)
                .HasColumnName("account_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(x => x.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .IsRequired();

            builder.Property(x => x.Passwrd)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnName("phone")
                .IsRequired();

            builder.Property(x => x.PhoneConfirmed)
                .HasColumnName("phone_confirmed")
                .IsRequired();

            builder.Property(x => x.Role)
                .HasColumnName("role")
                .IsRequired();

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .IsRequired();

            builder.Property(x => x.AccessTokenValue)
                .HasColumnName("access_token_value");

            builder.Property(x => x.AccessTokenExpiresIn)
                .HasColumnName("access_token_expires_in");

            builder.Property(x => x.RefreshTokenValue)
                .HasColumnName("refresh_token_value");

            builder.Property(x => x.RefreshTokenExpiresIn)
                .HasColumnName("refresh_token_expires_in");

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
