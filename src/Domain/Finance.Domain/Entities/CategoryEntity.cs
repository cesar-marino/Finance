using Finance.Domain.Enums;
using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class CategoryEntity : AggregateRoot
    {
        public bool Active { get; private set; }
        public CategoryType? CategoryType { get; private set; }
        public string Name { get; private set; }
        public string? Icon { get; private set; }
        public string? Color { get; private set; }
        public Guid? SuperCategoryId { get; private set; }

        public CategoryEntity(
            Guid accountId,
            CategoryType? categoryType,
            string name,
            string? icon,
            string? color,
            Guid? superCategoryId = null) : base(accountId: accountId)
        {
            Active = true;
            CategoryType = categoryType;
            Name = name;
            Icon = icon;
            Color = color;
            SuperCategoryId = superCategoryId;
        }

        public CategoryEntity(
            Guid accountId,
            Guid categoryId,
            bool active,
            CategoryType? categoryType,
            string name,
            string? icon,
            string? color,
            Guid? superCategoryId,
            DateTime createdAt,
            DateTime updatedAt) : base(accountId, categoryId, createdAt, updatedAt)
        {
            Active = active;
            CategoryType = categoryType;
            Name = name;
            Icon = icon;
            Color = color;
            SuperCategoryId = superCategoryId;
        }

        public void Disable()
        {
            Active = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Enable()
        {
            Active = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Updated(
            bool active,
            CategoryType categoryType,
            string name,
            string? icon,
            string? color)
        {
            Active = active;
            CategoryType = categoryType;
            Name = name;
            Icon = icon;
            Color = color;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
