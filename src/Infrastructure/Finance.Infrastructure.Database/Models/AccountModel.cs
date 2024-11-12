using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Infrastructure.Database.Models
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string Passwrd { get; set; }
        public string? AccessTokenValue { get; set; }
        public DateTime? AccessTokenExpiresIn { get; set; }
        public string? RefreshTokenValue { get; set; }
        public DateTime? RefreshTokenExpiresIn { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TagModel>? Tags { get; set; }
        public List<CategoryModel>? Categories { get; set; }
        public List<LimitModel>? Limits { get; set; }

        public AccountModel(
            Guid accountId,
            bool active,
            string username,
            string email,
            bool emailConfirmed,
            string? phone,
            bool phoneConfirmed,
            string password,
            string? accessTokenValue,
            DateTime? accessTokenExpiresIn,
            string? refreshTokenValue,
            DateTime? refreshTokenExpiresIn,
            string role,
            DateTime createdAt,
            DateTime updatedAt)
        {
            AccountId = accountId;
            Active = active;
            Username = username;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Phone = phone;
            PhoneConfirmed = phoneConfirmed;
            Passwrd = password;
            AccessTokenValue = accessTokenValue;
            AccessTokenExpiresIn = accessTokenExpiresIn;
            RefreshTokenValue = refreshTokenValue;
            RefreshTokenExpiresIn = refreshTokenExpiresIn;
            Role = role;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static AccountModel FromEntity(AccountEntity account) => new(
            accountId: account.Id,
            active: account.Active,
            username: account.Username,
            email: account.Email,
            emailConfirmed: account.EmailConfirmed,
            phone: account.Phone,
            phoneConfirmed: account.PhoneConfirmed,
            password: account.Password,
            accessTokenValue: account.AccessToken?.Value,
            accessTokenExpiresIn: account.AccessToken?.ExpiresIn,
            refreshTokenValue: account.RefreshToken?.Value,
            refreshTokenExpiresIn: account.RefreshToken?.ExpiresIn,
            role: account.Role.ToString(),
            createdAt: account.CreatedAt,
            updatedAt: account.UpdatedAt);

        public AccountEntity ToEntity()
        {
            return new AccountEntity(
                accountId: AccountId,
                active: Active,
                username: Username,
                email: Email,
                emailConfirmed: EmailConfirmed,
                phone: Phone,
                phoneConfirmed: PhoneConfirmed,
                password: Passwrd,
                role: (Roles)Enum.Parse(typeof(Roles), Role),
                accessToken: (AccessTokenValue != null && AccessTokenExpiresIn != null) ? new(AccessTokenValue, AccessTokenExpiresIn.Value) : null,
                refreshToken: (RefreshTokenValue != null && RefreshTokenExpiresIn != null) ? new(RefreshTokenValue, RefreshTokenExpiresIn.Value) : null,
                createdAt: CreatedAt,
                updatedAt: UpdatedAt);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected AccountModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
