using Finance.Domain.Entities;
using Finance.Domain.ValueObjects;

namespace Finance.Application.Services
{
    public interface ITokenService
    {
        Task<UserToken> GenerateAccessTokenAsync(
            UserEntity user,
            CancellationToken cancellationToken = default);

        Task<UserToken> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default);

        Task<string> GetUsernameFromTokenAsync(
            string token,
            CancellationToken cancellationToken = default);
    }
}