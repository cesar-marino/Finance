using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Tag.Commons
{
    public class TagResponse(
        Guid accountId,
        Guid tagId,
        bool active,
        string name,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid AccountId { get; } = accountId;
        public Guid TagId { get; private set; } = tagId;
        public bool Active { get; private set; } = active;
        public string Name { get; private set; } = name;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public DateTime UpdatedAt { get; private set; } = updatedAt;

        public static TagResponse FromEntity(TagEntity tag) => new(
            accountId: tag.AccountId,
            tagId: tag.Id,
            active: tag.Active,
            name: tag.Name,
            createdAt: tag.CreatedAt,
            updatedAt: tag.UpdatedAt);
    }
}
