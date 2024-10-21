using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class TagEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string Name { get; private set; }

        public TagEntity(Guid accountId, string name) : base(accountId: accountId)
        {
            Active = true;
            Name = name;
        }

        public TagEntity(
            Guid accountId,
            Guid tagId,
            bool active,
            string name,
            DateTime createdAt,
            DateTime updatedAt) : base(accountId, tagId, createdAt, updatedAt)
        {
            Active = active;
            Name = name;
        }

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Enable()
        {
            Active = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeName(string name)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
