namespace GroceryStoreApi.Domain
{
    public class Order
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public bool IsClosed { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

