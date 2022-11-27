using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApi.Domain
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public double Price { get; set; } // Product unitary price value

        public string? Description { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

