namespace GameStore.Domain.Entities.Carts
{
    internal class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public int Count { get; set; }
    }
}
