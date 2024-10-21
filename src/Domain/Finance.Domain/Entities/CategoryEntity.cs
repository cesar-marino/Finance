using Finance.Domain.SeedWork;

namespace Finance.Domain.Entities
{
    public class CategoryEntity : AggregateRoot
    {
        public bool Active { get; }
        public string Name { get; }
        public string? Icon { get; }
        public string? Color { get; }

        private readonly List<Guid> _subcategories = [];
        public IReadOnlyList<Guid> Subcategories => _subcategories.AsReadOnly();

        public CategoryEntity(string name, string? icon, string? color)
        {
            Active = true;
            Name = name;
            Icon = icon;
            Color = color;
            _subcategories = [];
        }

        public CategoryEntity(
            Guid categoryId,
            bool active,
            string name,
            string? icon,
            string? color,
            IReadOnlyList<Guid> subcategories,
            DateTime createdAt,
            DateTime updatedAt) : base(categoryId, createdAt, updatedAt)
        {
            Active = active;
            Name = name;
            Icon = icon;
            Color = color;
            _subcategories = subcategories.ToList();
        }
    }
}
