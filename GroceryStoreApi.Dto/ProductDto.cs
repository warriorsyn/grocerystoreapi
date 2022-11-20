using GroceryStoreApi.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace GroceryStoreApi.Dto
{
    public class ProductDto
    {
        // Constructor methods

        /*
         * Required class constructor to initialize new objects
         */
        public ProductDto() {  }

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

        // Attributes / Fields

        [Key]
        [Description("Demonstrates the product unique identification (ID) in the system")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid product")]
        public long Id { get; set; }

        [Required(ErrorMessage = $"{nameof(Name)} field is required")]
        [Description("Demonstrates the product name in the system")]
        [MinLength(3, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [MaxLength(60, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [DefaultValue("Empty product name")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "This field must be equals or higher than 0")]
        [Description("Demonstrates the avalaible product amount (Stock)")]
        [DefaultValue(0)]
        public int Stock { get; set; }

        [Required(ErrorMessage = $"{nameof(Price)} field is required")]
        [Description("Demonstrates the price value per product")]
        [Range(0.01, double.MaxValue, ErrorMessage = "This field must be higher than 0")]
        public double Price { get; set; }

        [StringLength(1024, ErrorMessage = $"The {nameof(Description)} field must contains 1024 characters max lenght")]
        [Description("Demonstrates the product description in the system")]
        [DefaultValue("Empty product description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = $"{nameof(CategoryId)} field is required")]
        [Description("Demonstrates the category unique identification (ID) for the referred product in the system")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid category")]
        public long CategoryId { get; set; }

        [Description("Demonstrates the coupled category for the referred product in the system")]
        public Category Category { get; set; }
    }
}
