using System.ComponentModel.DataAnnotations;

namespace LiteraLog.Models.DTOs.Users
{
    public class UserRegistrationDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 100 ,MinimumLength = 8, ErrorMessage = "The password should be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters and contain at least one uppercase letter, one lowercase letter, and one symbol.")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
