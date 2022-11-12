using GroceryStoreApi.Domain;

namespace GroceryStoreApi.Dto
{
    public class UserDto
    {
        public UserDto() { }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name ?? throw new ArgumentNullException(nameof(user.Name));
            Email = user.Email;
            Password = user.Password;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}