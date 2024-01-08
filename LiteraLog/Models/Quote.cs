namespace LiteraLog.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public User user { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public ICollection<QuoteTags> QuoteTag { get; set; }
        public int UserId { get; set; }
    }
}
