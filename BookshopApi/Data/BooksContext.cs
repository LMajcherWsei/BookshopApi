using BookshopApi.Data.Config;
using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookshopApi.Data
{
    //public class BooksContext : DbContext
    public class BooksContext : IdentityDbContext<AppUser>
    {
/*        public BooksContext()
        {
        }*/

        public BooksContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

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
