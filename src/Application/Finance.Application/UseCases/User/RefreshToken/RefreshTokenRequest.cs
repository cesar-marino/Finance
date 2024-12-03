using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.RefreshToken
{
    public class RefreshTokenRequest(
        string accessToken,
        string refreshToken) : IRequest<UserResponse>
    {
        public string AccessToken { get; } = accessToken;
        public string RefreshToken { get; } = refreshToken;
    }
}