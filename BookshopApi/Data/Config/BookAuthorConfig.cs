using BookshopApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookshopApi.Data.Config
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(e => new { e.AuthorId, e.BookId });

            builder.ToTable("BooksAuthors");

            builder.Property(e => e.AuthorId).HasColumnName("author_id");

            builder.Property(e => e.BookId).HasColumnName("book_id");

            builder.HasOne(d => d.Author)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_BookAuthor_author");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_BookAuthor_book");

  /*          builder.HasData(new List<BookAuthor>()
            {
                new()
                {
                    AuthorId = 1,
                    BookId = 1,
                },
                new()
                {
                    AuthorId = 2,
                    BookId = 2,
                },
            });*/
        }
    }
}
