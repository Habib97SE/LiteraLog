using Microsoft.EntityFrameworkCore;

namespace LiteraLog.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
