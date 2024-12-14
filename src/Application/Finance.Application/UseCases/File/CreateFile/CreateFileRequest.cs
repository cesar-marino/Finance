using MediatR;

namespace Finance.Application.UseCases.File.CreateFile
{
    public class CreateFileRequest(string name, Stream? video) : IRequest<bool>
    {
        public string Name { get; } = name;
        public Stream? Video { get; } = video;
    }
}