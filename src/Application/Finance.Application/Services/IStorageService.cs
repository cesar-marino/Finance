namespace Finance.Application.Services
{
    public interface IStorageService
    {
        Task<string> Upload(byte[] file, CancellationToken cancellationToken = default);
    }
}