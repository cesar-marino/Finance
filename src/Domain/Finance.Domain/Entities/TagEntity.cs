using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class TagEntity : AuditableAggregateRoot
    {
        public bool Active { get; private set; }
        public string Name { get; private set; }

        public TagEntity(
            Guid userId,
            string name) : base(userId: userId)
        {
            Active = true;
            Name = name;
        }

        public TagEntity(
            Guid tagId,
            Guid userId,
            bool active,
            string name,
            DateTime createdAt,
            DateTime updatedAt) : base(id: tagId, userId: userId, createdAt: createdAt, updatedAt: updatedAt)
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
