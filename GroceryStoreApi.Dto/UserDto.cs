using GroceryStoreApi.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApi.Dto
{
    public class UserDto
    {
        // Constructor methods

        /*
         * Required class constructor to initialize new objects
         */
        public UserDto() { }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name ?? throw new ArgumentNullException(nameof(user.Name));
            Email = user.Email;
            IsAdmin = user.IsAdmin;
        }

        /*
         * All args constructor
         */
        public UserDto(long id, string name, string email, bool? isAdmin = false)
        {
            Id = id;
            Name = name;
            Email = email;
            IsAdmin = isAdmin.GetValueOrDefault();
        }

        // Attributes / Fields

        [Key]
        [Description("Demonstrates the user unique identification (ID) in the system")]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid user")]
        public long Id { get; set; }

        [Required(ErrorMessage = $"{nameof(Name)} field is required")]
        [Description("Demonstrates the user name in the system")]
        [MinLength(3, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [MaxLength(60, ErrorMessage = "This field must contains a length between 3 and 60 characters")]
        [DefaultValue("Empty username")]
        public string Name { get; set; }

        [Required(ErrorMessage = $"{nameof(Email)} field is required")]
        [Description("Demonstrates an E-mail field for user authentication and security purposes")]
        [EmailAddress(ErrorMessage = "Invalid Email Adress")]
        public string? Email { get; set; }

        [Required(ErrorMessage = $"{nameof(Password)} field is required")]
        [Description("Demonstrates a password field for user authentication and security purposes")]
        [MinLength(6, ErrorMessage = "This field must contains a length between 6 and 18 characters")]
        [MaxLength(18, ErrorMessage = "This field must contains a length between 6 and 18 characters")]
        public string? Password { get; set; }

        [Description("Demonstrates a flag field to check if an user has administrator permissions or not")]
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
    }
}