using Finance.Application.UseCases.Account.Commons;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandler : IRefreshTokenHandler
    {
        public Task<AccountResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}