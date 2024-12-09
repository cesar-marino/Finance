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
            string? color,
            string? logo)
        {
            Active = true;
            Code = code;
            Name = name;
            Color = color;
            Logo = logo;
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
    }
}