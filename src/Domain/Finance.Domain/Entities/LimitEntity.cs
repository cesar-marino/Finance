using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class LimitEntity : AggregateRoot
    {
        public string Name { get; private set; }
        public double LimitAmount { get; private set; }
        public Guid CategoryId { get; private set; }

        public LimitEntity(
            Guid accountId,
            Guid categoryId,
            string name,
            double limitAmount) : base(accountId)
        {
            CategoryId = categoryId;
            Name = name;
            LimitAmount = limitAmount;
        }

        public LimitEntity(
            Guid accountId,
            Guid limitId,
            Guid categoryId,
            string name,
            double limitAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(accountId, limitId, createdAt, updatedAt)
        {
            CategoryId = categoryId;
            Name = name;
            LimitAmount = limitAmount;
        }

        public void Update(string name, double limitAmount, Guid categoryId)
        {
            Name = name;
            LimitAmount = limitAmount;
            CategoryId = categoryId;
        }
    }
}
