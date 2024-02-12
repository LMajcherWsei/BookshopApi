using BookshopApi.Data.Config;
using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopApi.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table Books
            modelBuilder.ApplyConfiguration(new BookConfig());

            // Table Authors
            modelBuilder.ApplyConfiguration(new AuthorConfig());

            // Table BookAuthor
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());

            // Table Publishers
            modelBuilder.ApplyConfiguration(new PublisherConfig());

        }
}
}
