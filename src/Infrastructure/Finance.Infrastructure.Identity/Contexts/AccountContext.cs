using Finance.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Identity.Contexts
{
    public class AccountContext(DbContextOptions options) : IdentityDbContext<AccountModel, RoleModel, Guid>(options)
    {
    }
}