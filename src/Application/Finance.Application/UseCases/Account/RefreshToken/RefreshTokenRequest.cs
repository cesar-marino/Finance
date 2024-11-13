using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenRequest(
        string accessToken,
        string refreshToken) : IRequest<AccountResponse>
    {
        public string AccessToken { get; } = accessToken;
        public string RefreshToken { get; } = refreshToken;
    }
}