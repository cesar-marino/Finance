using Finance.Domain.Entities;

namespace Finance.Infrastructure.Database.Models
{
    public class TagModel(
        Guid userId,
        Guid tagId,
        bool active,
        string name,
        string normalizedName,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid TagId { get; set; } = tagId;
        public bool Active { get; set; } = active;
        public string Name { get; set; } = name;
        public string NormalizedName { get; set; } = normalizedName;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime UpdatedAt { get; set; } = updatedAt;

        public Guid UserId { get; set; } = userId;
        public virtual UserModel? User { get; set; }

        public static TagModel FromEntity(TagEntity tag) => new(
            userId: tag.UserId,
            tagId: tag.Id,
            active: tag.Active,
            name: tag.Name,
            normalizedName: tag.Name.ToUpper().Trim(),
            createdAt: tag.CreatedAt,
            updatedAt: tag.UpdatedAt);

        public TagEntity ToEntity() => new(
            userId: UserId,
            tagId: TagId,
            active: Active,
            name: Name,
            createdAt: CreatedAt,
            updatedAt: UpdatedAt);
    }
}
