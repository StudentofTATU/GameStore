using GameStore.Domain.Entities.Games;

namespace GameStore.Contracts.Games
{
    public class GameDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public Guid OwnerId { get; set; }

        public GameDTO()
        { }

        public GameDTO(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            Description = game.Description;
            Price = game.Price;
            ImageUrl = game.ImageUrl;
            CreatedDateTime = game.CreatedDateTime;
            OwnerId = game.OwnerId;
        }
        public GameDTO(Guid id, string name, string description, double price, string imageUrl, DateTimeOffset createdDateTime, Guid ownerId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            CreatedDateTime = createdDateTime;
            OwnerId = ownerId;
        }
    }
}
