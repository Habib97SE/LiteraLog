using System.ComponentModel.DataAnnotations;

namespace LiteraLog.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(999)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        public string CoverImage { get; set; }
        [Required]
        [StringLength(15)]
        public string ISBN { get; set; }
        
        public int Pages { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int Rating { get; set; }
        public string comment { get; set; }
        public DateTime publishedYear { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set;}


    }
}
