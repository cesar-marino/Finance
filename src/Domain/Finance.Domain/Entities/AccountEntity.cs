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

        public AccountEntity(
            string username,
            string email,
            string password,
            string? phone = null)
        {
            Active = true;
            Username = username;
            Email = email;
            EmailConfirmed = false;
            Phone = phone;
            PhoneConfirmed = false;
            Password = password;
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
        }
    }
}