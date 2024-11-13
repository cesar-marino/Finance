using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Finance.Application.Services;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Infrastructure.Services.TokenService
{
    public class JwtBearerAdapter(IConfiguration configuration) : ITokenService
    {
        public async Task<AccountToken> GenerateAccessTokenAsync(AccountEntity account, CancellationToken cancellationToken = default)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
                _ = int.TryParse(configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, account.Username),
                    new(ClaimTypes.Email, account.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var accessTokenExpiration = DateTime.Now.AddMinutes(accessTokenValidityInMinutes);

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: accessTokenExpiration,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                return await Task.Run(() => new AccountToken(accessToken, accessTokenExpiration), cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public Task<AccountToken> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUsernameFromTokenAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}