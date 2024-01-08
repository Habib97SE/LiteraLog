using System.ComponentModel.DataAnnotations;

namespace LiteraLog.Models.DTOs.Books
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime PublishedYear { get; set; }
        public int UserId { get; set; }

    }
}
