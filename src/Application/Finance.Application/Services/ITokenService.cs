using Finance.Domain.Entities;
using Finance.Domain.ValueObjects;

namespace Finance.Application.Services
{
    public interface ITokenService
    {
        Task<AccountToken> GenerateAccessTokenAsync(
            AccountEntity account,
            CancellationToken cancellationToken = default);

        Task<AccountToken> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default);

        Task<string> GetUsernameFromTokenAsync(string token, CancellationToken cancellationToken);
    }
}