namespace GameStore.Contracts.Games
{
    public class CreateGameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public Guid OwnerId { get; set; }

    }
}
