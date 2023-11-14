namespace GameStore.Domain.Entities.Categories
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GameCategory>? GameCategories { get; set; } = new();
    }
}
