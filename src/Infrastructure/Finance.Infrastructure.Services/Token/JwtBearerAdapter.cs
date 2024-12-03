using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Finance.Application.Services;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Infrastructure.Services.Token
{
    public class JwtBearerAdapter(IConfiguration configuration) : ITokenService
    {
        public async Task<UserToken> GenerateAccessTokenAsync(UserEntity user, CancellationToken cancellationToken = default)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
                _ = int.TryParse(configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
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
                return await Task.Run(() => new UserToken(accessToken, accessTokenExpiration), cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<UserToken> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _ = int.TryParse(configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
                var refreshTokenExpiration = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return await Task.Run(() => new UserToken(Convert.ToBase64String(randomNumber), refreshTokenExpiration), cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public async Task<string> GetUsernameFromTokenAsync(string token, CancellationToken cancellationToken)
        {
            try
            {
                return await Task.Run(string () =>
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)),
                        ValidateLifetime = false
                    };

                    var principal = new JwtSecurityTokenHandler().ValidateToken(
                        token,
                        tokenValidationParameters,
                        out SecurityToken securityToken);

                    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new SecurityTokenException("Invalid token");
                    }

                    return principal.Identity?.Name ?? throw new SecurityTokenException("Invalid token");
                });
            }
            catch (Exception ex)
            {
                throw new InvalidTokenException(innerException: ex);
            }
        }
    }
}