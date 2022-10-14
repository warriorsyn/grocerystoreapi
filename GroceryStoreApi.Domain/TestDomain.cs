using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApi.Domain
{
    public  class TestDomain
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
