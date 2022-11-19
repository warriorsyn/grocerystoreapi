namespace GroceryStoreApi.Dto
{
    public class OrderDto
    {
        public long Id { get; set; }

        public UserDto User { get; set; }

        public ProductDto Product { get; set; }

        public bool IsClosed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
