using MediatR;

namespace Finance.Application.UseCases.File.CreateFile
{
    public interface ICreateFileHandler : IRequestHandler<CreateFileRequest, bool>
    {

    }
}