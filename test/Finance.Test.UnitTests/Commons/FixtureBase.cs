using Bogus;
using Finance.Domain.Entities;

namespace Finance.Test.UnitTests.Commons
{
    public abstract class FixtureBase
    {
        public CancellationToken CancellationToken { get; } = CancellationToken.None;
        public Faker Faker { get; } = new("pt_BR");

        public TagEntity MakeTagEntity(
            Guid? tagId = null,
            bool active = true,
            string? name = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) => new TagEntity(
                tagId: tagId ?? Faker.Random.Guid(),
                active: active,
                name: name ?? Faker.Random.String(5),
                createdAt: createdAt ?? Faker.Date.Past(),
                updatedAt: updatedAt ?? Faker.Date.Past());
    }
}
