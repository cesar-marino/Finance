using Microsoft.AspNetCore.Identity;

namespace Finance.Infrastructure.Identity.Models
{
    public class AccountModel : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}