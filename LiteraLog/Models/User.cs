using System.ComponentModel.DataAnnotations;

namespace LiteraLog.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The first name field cannot be empty.")]
        [StringLength(50, MinimumLength = 3 ,ErrorMessage = "The first name field should be at least 3 characters.")]
        [RegularExpression(@"^[a-zA-Z'-]*$", ErrorMessage = "Only alphabets, hyphens, and apostrophes are allowed.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The last name field cannot be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The last name field should be at least 3 characters.")]
        [RegularExpression(@"^[a-zA-Z'-]*$", ErrorMessage = "Only alphabets, hyphens, and apostrophes are allowed.")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The email address field cannot be empty.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The password field cannot be empty.")]
        public string Password { get; set; }

        public string ProfileImage { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public User () 
        {
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            Books = new HashSet<Book>();
            Quotes = new HashSet<Quote>();
        }
    }
}
