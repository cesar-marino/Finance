using Finance.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            throw new NotImplementedException();
        }
    }
}
