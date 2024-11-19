using Finance.Application.Services;
using Finance.Domain.Exceptions;

namespace Finance.Infrastructure.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        public Task<string> EcnryptAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(BCrypt.Net.BCrypt.EnhancedHashPassword(key, 13, BCrypt.Net.HashType.SHA512));
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }

        public Task<bool> VerifyAsync(string value, string hash, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(BCrypt.Net.BCrypt.EnhancedVerify(value, hash, BCrypt.Net.HashType.SHA512));
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(innerException: ex);
            }
        }
    }
}