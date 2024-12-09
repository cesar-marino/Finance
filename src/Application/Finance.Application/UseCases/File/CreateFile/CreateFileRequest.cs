using MediatR;

namespace Finance.Application.UseCases.File.CreateFile
{
    public class CreateFileRequest(string name, Stream Video) : IRequest<bool>
    {
        public string Name { get; } = name;
        Stream Video { get; } = Video;
    }
}