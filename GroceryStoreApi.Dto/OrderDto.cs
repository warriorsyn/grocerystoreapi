using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GroceryStoreApi.Dto
{
    public class OrderDto
    {
        // Attributes / Fields

        [Key]
        [Description("Demonstrates the order unique identification (ID) in the system")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid order")]
        public long Id { get; set; }

        public UserDto User { get; set; }

        public ProductDto Product { get; set; }

        [Description("Demonstrates a flag field to check if an order has been closed or not")]
        [DefaultValue(false)]
        public bool IsClosed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
