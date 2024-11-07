namespace Finance.Domain.Entities
{
    public class AccountEntity
    {
        public Guid AccountId { get; }
        public bool Active { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        public AccountEntity(
            string username,
            string email,
            string password)
        {
            Active = true;
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public AccountEntity(
            Guid accountId,
            bool active,
            string username,
            string email,
            string password,
            DateTime createdAt,
            DateTime updatedAt)
        {
            AccountId = accountId;
            Active = active;
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}