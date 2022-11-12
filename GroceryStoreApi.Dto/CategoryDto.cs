using GroceryStoreApi.Domain;

namespace GroceryStoreApi.Dto
{
    public class CategoryDto
    {
        public CategoryDto() { }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name ?? throw new ArgumentNullException(nameof(category.Name));
            Description = category.Description;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
