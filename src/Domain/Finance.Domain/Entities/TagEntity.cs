using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class TagEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string Name { get; private set; }

        public TagEntity(
            Guid accountId,
            string name) : base(accountId: accountId)
        {
            Active = true;
            Name = name;
        }

        public TagEntity(
            Guid tagId,
            bool active,
            string name,
            Guid accountId,
            DateTime createdAt,
            DateTime updatedAt) : base(
                accountId: accountId,
                id: tagId,
                createdAt: createdAt,
                updatedAt: updatedAt)
        {
            Active = active;
            Name = name;
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
