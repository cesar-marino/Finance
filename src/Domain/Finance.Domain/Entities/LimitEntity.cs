﻿using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class LimitEntity : AggregateRoot
    {
        public Guid AccountId { get; }
        public string Name { get; private set; }
        public double LimitAmount { get; private set; }
        public Guid CategoryId { get; private set; }

        public LimitEntity(
            Guid accountId,
            Guid categoryId,
            string name,
            double limitAmount)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            Name = name;
            LimitAmount = limitAmount;
        }

        public LimitEntity(
            Guid limitId,
            Guid accountId,
            Guid categoryId,
            string name,
            double limitAmount,
            DateTime createdAt,
            DateTime updatedAt) : base(limitId, createdAt, updatedAt)
        {
            AccountId = accountId;
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
