using Finance.Domain.Enums;
using Finance.Domain.SeedWork;
using Finance.Domain.ValueObjects;

namespace Finance.Domain.Entities
{
    public class UserEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string? Phone { get; private set; }
        public bool PhoneConfirmed { get; private set; }
        public string Password { get; private set; }
        public UserToken? AccessToken { get; private set; }
        public UserToken? RefreshToken { get; private set; }
        public Roles Role { get; private set; }

        public UserEntity(
            string username,
            string email,
            string password,
            string? phone = null,
            Roles? role = null)
        {
            Active = true;
            Username = username;
            Email = email;
            EmailConfirmed = false;
            Phone = phone;
            PhoneConfirmed = false;
            Password = password;
            Role = role ?? Roles.User;
        }

        public UserEntity(
            Guid userId,
            bool active,
            string username,
            string email,
            bool emailConfirmed,
            string? phone,
            bool phoneConfirmed,
            string password,
            Roles role,
            UserToken? accessToken,
            UserToken? refreshToken,
            DateTime createdAt,
            DateTime updatedAt) : base(userId, createdAt, updatedAt)
        {
            Active = active;
            Username = username;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Phone = phone;
            PhoneConfirmed = phoneConfirmed;
            Password = password;
            Role = role;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void ChangeTokens(
            UserToken accessToken,
            UserToken refreshToken)
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

        public void Enable()
        {
            Active = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RevokeTokens()
        {
            AccessToken = null;
            RefreshToken = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string password)
        {
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}