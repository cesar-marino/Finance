using Finance.Application.Services;
using Finance.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Finance.Infrastructure.Identity.Services
{
    public class AccountManagerAdapter(UserManager<AccountModel> userManager) : IAccountService
    {
        public async Task CreateAsync(
            Guid accountId,
            string email,
            string username,
            string password)
        {
            var user = new AccountModel
            {
                CreatedAt = DateTime.UtcNow,
                Email = email,
                Id = accountId,
                EmailConfirmed = false,
                UpdatedAt = DateTime.UtcNow,
                UserName = username,
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                //handler errors
            }

            //return user.toEntity()

            throw new NotImplementedException();
        }
    }
}