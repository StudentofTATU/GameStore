using GameStore.Domain.Entities.Categories;

namespace GameStore.Domain.Entities.Games
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public Guid OwnerId { get; set; }

        public List<GameCategory>? GameCategories { get; set; } = new();

    }
}
