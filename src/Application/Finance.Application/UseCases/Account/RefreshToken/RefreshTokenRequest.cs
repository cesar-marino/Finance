using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenRequest(string accessToken) : IRequest<AccountResponse>
    {
        public string AccessToken { get; } = accessToken;
    }
}