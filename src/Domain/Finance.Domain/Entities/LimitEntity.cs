using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class LimitEntity : AggregateRoot
    {
        public string Name { get; private set; }
        public double CurrentAmount { get; private set; }
        public double LimitAmount { get; private set; }

        public LimitEntity(
            Guid accountId,
            string name,
            double limitAmount) : base(accountId)
        {
            Name = name;
            LimitAmount = limitAmount;
            CurrentAmount = 0;
        }

        public LimitEntity(
            Guid accountId,
            Guid limitId,
            string name,
            double currentAmount,
            double limitAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(accountId, limitId, createdAt, updatedAt)
        {
            Name = name;
            LimitAmount = limitAmount;
            CurrentAmount = currentAmount;
        }
    }
}
