using Finance.Domain.Enums;
using Finance.Domain.SeedWork;
using Finance.Domain.ValueObjects;

namespace Finance.Domain.Entities
{
    public class AccountEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string? Phone { get; private set; }
        public bool PhoneConfirmed { get; private set; }
        public string Password { get; private set; }
        public AccountToken? AccessToken { get; private set; }
        public AccountToken? RefreshToken { get; private set; }
        public Role Role { get; private set; }

        public AccountEntity(
            string username,
            string email,
            string password,
            string? phone = null,
            Role? role = null)
        {
            Active = true;
            Username = username;
            Email = email;
            EmailConfirmed = false;
            Phone = phone;
            PhoneConfirmed = false;
            Password = password;
            Role = role ?? Role.User;
        }

        public AccountEntity(
            Guid accountId,
            bool active,
            string username,
            string email,
            bool emailConfirmed,
            string? phone,
            bool phoneConfirmed,
            string password,
            Role role,
            DateTime createdAt,
            DateTime updatedAt) : base(accountId, createdAt, updatedAt)
        {
            Active = active;
            Username = username;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Phone = phone;
            PhoneConfirmed = phoneConfirmed;
            Password = password;
            Role = role;
        }

        public void ChangeTokens(
            AccountToken accessToken,
            AccountToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            EmailConfirmed = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeUsername(string username)
        {
            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}