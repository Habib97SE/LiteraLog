﻿using System.ComponentModel.DataAnnotations;

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
        public float Rating { get; set; }
        public string Comment { get; set; }
        public User user { get; set; }
        public DateTime PublishedYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int UserId { get; set; } 

        public Book()
        {
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
