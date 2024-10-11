namespace Finance.Domain.SeedWork
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        protected EntityBase(
            Guid? id = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
        }
    }
}
