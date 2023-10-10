namespace GameStore.Domain.Entities.Orders
{
    internal class Order
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
