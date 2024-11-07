using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Account.Commons
{
    public class AccountResponse(
        Guid accountId,
        bool active,
        string username,
        string email,
        bool emailConfirmed,
        string? phone,
        bool phoneConfirmed,
        TokenResponse? accessToken,
        TokenResponse? refreshToken,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid AccountId { get; } = accountId;
        public bool Active { get; } = active;
        public string Username { get; } = username;
        public string Email { get; } = email;
        public bool EmailConfirmed { get; } = emailConfirmed;
        public string? Phone { get; } = phone;
        public bool PhoneConfirmed { get; } = phoneConfirmed;
        public TokenResponse? AccessToken { get; } = accessToken;
        public TokenResponse? RefreshToken { get; } = refreshToken;
        public DateTime CreatdAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static AccountResponse FromEntity(AccountEntity account) => new(
            accountId: account.Id,
            active: account.Active,
            username: account.Username,
            email: account.Email,
            emailConfirmed: account.EmailConfirmed,
            phone: account.Phone,
            phoneConfirmed: account.PhoneConfirmed,
            accessToken: account.AccessToken != null ? new(account.AccessToken.Value, account.AccessToken.ExpiresIn) : null,
            refreshToken: account.RefreshToken != null ? new(account.RefreshToken.Value, account.RefreshToken.ExpiresIn) : null,
            createdAt: account.CreatedAt,
            updatedAt: account.UpdatedAt);
    }

    public class TokenResponse(
        string value,
        DateTime expireIn)
    {
        public string Value { get; } = value;
        public DateTime ExpiresIn { get; } = expireIn;
    }
}