using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Tag.Commons
{
    public class TagResponse(
        Guid userId,
        Guid tagId,
        bool active,
        string name,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid UserId { get; } = userId;
        public Guid TagId { get; } = tagId;
        public bool Active { get; } = active;
        public string Name { get; } = name;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static TagResponse FromEntity(TagEntity tag) => new(
            userId: tag.UserId,
            tagId: tag.Id,
            active: tag.Active,
            name: tag.Name,
            createdAt: tag.CreatedAt,
            updatedAt: tag.UpdatedAt);
    }
}
