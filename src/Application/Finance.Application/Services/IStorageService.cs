namespace Finance.Application.Services
{
    public interface IStorageService
    {
        Task<string> UploadAsync(string path, byte[] file, CancellationToken cancellationToken = default);
    }
}