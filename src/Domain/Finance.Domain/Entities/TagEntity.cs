using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class TagEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string Name { get; private set; }

        public TagEntity(string name)
        {
            Active = true;
            Name = name;
        }

        public TagEntity(
            Guid tagId,
            bool active,
            string name,
            DateTime createdAt,
            DateTime updatedAt) : base(tagId, createdAt, updatedAt)
        {
            Active = active;
            Name = name;
        }

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
