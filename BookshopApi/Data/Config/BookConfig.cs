using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BookshopApi.Data.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .UseIdentityColumn()
                .HasColumnName("book_id");

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("title")
                .IsUnicode(false);

            builder.Property(b => b.PhotoUrl)
                .HasMaxLength(100)
                .HasColumnName("photo_url");

            builder.Property(b => b.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");

            builder.Property(b => b.Pages)
                .IsRequired(false)
                .HasColumnName("pages");

            builder.Property(e => e.PublisherId)
                .HasColumnName("pub_id");

            builder.Property(b => b.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("money");

            builder.Property(e => e.Rate)
                .HasColumnName("rate");
            //.HasColumnType("decimal(5,2");

            builder.Property(e => e.Series)
                .HasColumnName("series")
                .HasMaxLength(250);

            builder.Property(b => b.PublicationDate)
                .IsRequired(false)
                .HasColumnName("published_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(b => b.Language)
                .HasColumnName("language")
                .HasMaxLength(50);

            builder.HasOne(d => d.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK_book_publisher");


            builder.HasData(new List<Book>()
            {
                new()
                {
                    Id = 1,
                    Title = "Książę",
                    PhotoUrl = "http://Ksiaze.com",
                    Description = "Książę",
                    Pages = 300,
                    PublisherId = 1,
                    Price = 22.22M,
                    Rate = 5.5,
                    Series = "",
                    PublicationDate = new DateTime(2020, 07, 22),
                    Language = "Polski"
                },
                new()
                {
                    Id = 2,
                    Title = "Ani słowa prawdy",
                    PhotoUrl = "http://Anislowaprawdy.com",
                    Description = "Ani słowa prawdy",
                    Pages = 300,
                    PublisherId = 2,
                    Price = 33.22M,
                    Rate = 5.5,
                    Series = "",
                    PublicationDate = new DateTime(2021, 04, 12),
                    Language = "Polski"
                }
            });
        }
    }
}
