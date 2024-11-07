using Finance.Domain.SeedWork;

namespace Finance.Domain.ValueObjects
{
    public class AccountToken(string value, DateTime expiresIn) : ValueObject
    {
        public string Value { get; } = value;
        public DateTime ExpiresIn { get; } = expiresIn;

        public override bool Equals(ValueObject? other) => other is AccountToken token
            && Value == token.Value
            && ExpiresIn == token.ExpiresIn;

        protected override int GetCustomHashCode() => HashCode.Combine(Value, ExpiresIn);
    }
}