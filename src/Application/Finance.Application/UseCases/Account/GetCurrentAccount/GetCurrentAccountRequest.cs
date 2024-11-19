using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountRequest(string accessToken) : IRequest<AccountResponse>
    {
        public string AccessToken { get; } = accessToken;
    }
}