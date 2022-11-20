using GroceryStoreApi.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApi.Dto
{
    public class CategoryDto
    {
        // Constructor methods

        /*
         * Required class constructor to initialize new objects
         */
        public CategoryDto() { }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name ?? throw new ArgumentNullException(nameof(category.Name));
            Description = category.Description;
        }

        // Attributes / Fields

        [Key]
        [Description("Demonstrates the category unique identification (ID) in the system")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid category")]
        public long Id { get; set; }

        [Required(ErrorMessage = $"{nameof(Name)} field is required")]
        [Description("Demonstrates the category name in the system")]
        [MinLength(3, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [MaxLength(60, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [DefaultValue("Empty category name")]
        public string Name { get; set; }

        [StringLength(1024, ErrorMessage = $"The {nameof(Description)} field must contains 1024 characters max lenght")]
        [Description("Demonstrates the product category description in the system")]
        [DefaultValue("Empty category description")]
        public string? Description { get; set; }
    }
}
