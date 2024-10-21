using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class TagEntity : AggregateRoot
    {
        public Guid AccountId { get; }
        public bool Active { get; private set; }
        public string Name { get; private set; }

        public TagEntity(Guid accountId, string name)
        {
            AccountId = accountId;
            Active = true;
            Name = name;
        }

        public TagEntity(
            Guid tagId,
            bool active,
            string name,
            Guid accountId,
            DateTime createdAt,
            DateTime updatedAt) : base(tagId, createdAt, updatedAt)
        {
            Active = active;
            Name = name;
            AccountId = accountId;
        }

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Enabled()
        {
            Active = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Updated(string name)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
