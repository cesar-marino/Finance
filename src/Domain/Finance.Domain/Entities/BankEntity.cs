using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class BankEntity : AggregateRoot
    {
        public string Name { get; private set; }
    }
}