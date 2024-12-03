using Finance.Domain.Entities;
using Finance.Domain.Enums;

namespace Finance.Application.UseCases.User.Commons
{
    public class UserResponse(
        Guid userId,
        bool active,
        string username,
        string email,
        bool emailConfirmed,
        string? phone,
        bool phoneConfirmed,
        TokenResponse? accessToken,
        TokenResponse? refreshToken,
        Roles role,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid UserId { get; } = userId;
        public bool Active { get; } = active;
        public string Username { get; } = username;
        public string Email { get; } = email;
        public bool EmailConfirmed { get; } = emailConfirmed;
        public string? Phone { get; } = phone;
        public bool PhoneConfirmed { get; } = phoneConfirmed;
        public TokenResponse? AccessToken { get; } = accessToken;
        public TokenResponse? RefreshToken { get; } = refreshToken;
        public Roles Role { get; } = role;
        public DateTime CreatdAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static UserResponse FromEntity(UserEntity user) => new(
            userId: user.Id,
            active: user.Active,
            username: user.Username,
            email: user.Email,
            emailConfirmed: user.EmailConfirmed,
            phone: user.Phone,
            phoneConfirmed: user.PhoneConfirmed,
            accessToken: user.AccessToken is not null ? new(user.AccessToken.Value, user.AccessToken.ExpiresIn) : null,
            refreshToken: user.RefreshToken is not null ? new(user.RefreshToken.Value, user.RefreshToken.ExpiresIn) : null,
            role: user.Role,
            createdAt: user.CreatedAt,
            updatedAt: user.UpdatedAt);
    }

    public class TokenResponse(
        string value,
        DateTime expireIn)
    {
        public string Value { get; } = value;
        public DateTime ExpiresIn { get; } = expireIn;
    }
}