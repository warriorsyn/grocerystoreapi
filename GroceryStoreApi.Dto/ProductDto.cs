using GroceryStoreApi.Domain;

namespace GroceryStoreApi.Dto
{
    public class ProductDto
    {
        public ProductDto()
        {
        }

        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Stock = product.Stock;
            Price = product.Price;
            Description = product.Description;
            CategoryId = product.CategoryId;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public long CategoryId { get; set; }
    }
}
