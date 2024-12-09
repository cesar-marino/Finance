using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class FileEntity : AggregateRoot
    {
        public string Name { get; }
        public string Path { get; }
        public string Extension { get; }
        public double Size { get; }

        public FileEntity(
            string name,
            string path,
            string extension,
            double size)
        {
            Name = name;
            Path = path;
            Extension = extension;
            Size = size;
        }

        public FileEntity(
            Guid fileId,
            string name,
            string path,
            string extension,
            double size,
            DateTime createdAt,
            DateTime updatedAt) : base(
                id: fileId,
                createdAt: createdAt,
                updatedAt: updatedAt)
        {
            Name = name;
            Path = path;
            Extension = extension;
            Size = size;
        }
    }
}