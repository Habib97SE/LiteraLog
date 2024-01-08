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
        public DbSet<QuoteTags> QuoteTags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations:
            modelBuilder.Entity<QuoteTags>()
                .HasKey(qt => new { qt.QuoteId, qt.TagId });

            modelBuilder.Entity<QuoteTags>()
                .HasOne(qt => qt.Quote)
                .WithMany(q => q.QuoteTag)
                .HasForeignKey(qt => qt.QuoteId);

            modelBuilder.Entity<QuoteTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuoteTag)
                .HasForeignKey(qt => qt.TagId);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.user)  
                .WithMany(u => u.Books) 
                .HasForeignKey(b => b.UserId); 
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.user)
                .WithMany(u => u.Quotes)
                .HasForeignKey(q => q.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
