namespace LiteraLog.Models.DTOs.Users
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public List<Book> Books { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
