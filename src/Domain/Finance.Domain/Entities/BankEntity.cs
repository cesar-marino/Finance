using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class BankEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public string? Code { get; private set; }
        public string Name { get; private set; }
        public string? Color { get; private set; }
        public string? Logo { get; private set; }

        public BankEntity(
            string? code,
            string name,
            string? color)
        {
            Active = true;
            Code = code;
            Name = name;
            Color = color;
        }

        public BankEntity(
            Guid bankId,
            bool active,
            string? code,
            string name,
            string? color,
            string? logo,
            DateTime createdAt,
            DateTime updatedAt) : base(
                id: bankId,
                createdAt: createdAt,
                updatedAt: updatedAt)
        {
            Active = active;
            Code = code;
            Name = name;
            Color = color;
            Logo = logo;
        }

        public void Update(
            string name,
            string? code,
            string? color)
        {
            Name = name;
            Code = code;
            Color = color;
            UpdatedAt = DateTime.UtcNow;
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
    }
}