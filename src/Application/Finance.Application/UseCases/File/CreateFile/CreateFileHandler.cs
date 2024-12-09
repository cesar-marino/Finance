
namespace Finance.Application.UseCases.File.CreateFile
{
    public class CreateFileHandler : ICreateFileHandler
    {
        public async Task<bool> Handle(CreateFileRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}