namespace Finance.Application.Services
{
    public interface IEncryptionService
    {
        Task<string> EcnryptAsync(
            string key,
            CancellationToken cancellationToken = default);

        Task<bool> VerifyAsync(
            string value,
            string hash,
            CancellationToken cancellationToken = default);
    }
}