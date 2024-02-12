using BookshopApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookshopApi.Data.Config
{
    public class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Publishers");

            builder.Property(e => e.Id)
                .HasColumnName("publisher_id");

            builder.Property(e => e.PublisherName)
                .IsRequired()
                .HasColumnName("publisher_name")
                .HasMaxLength(40)
            .IsUnicode(false);

            builder.HasData(new List<Publisher>()
            {
                new()
                {
                    Id = 1,
                    PublisherName = "PWN",
                },
                new()
                {
                    Id = 2,
                    PublisherName = "Runa",
                },
            });
        }
    }
}
