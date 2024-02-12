using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BookshopApi.Data.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("author_id")
                .UseIdentityColumn();

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("first_name");

            builder.Property(a => a.LastName)
                .IsRequired().HasMaxLength(50)
                .HasColumnName("last_name");

            builder.Property(a => a.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");

            builder.Property(a => a.Descripiton)
                .HasMaxLength(4000)
                .HasColumnName("description");

            builder.HasData(new List<Author>()
            {
                new()
                {
                    Id = 1,
                    FirstName = "Mikołaj",
                    MiddleName = "",
                    LastName = "Machiavelli",
                    Descripiton = "Prawnik, filozof, pisarz społeczny i polityczny, historyk i dyplomata florencki, jeden z najwybitniejszych przedstawicieli renesansowej myśli politycznej. Napisał traktat o sprawowaniu władzy pt. Książę, który sprawił, że od jego nazwiska powstał termin makiawelizm. Opisywał funkcjonowanie zarówno republik, jak i królestw. W 1559 jego pisma znalazły się na kościelnym indeksie ksiąg zakazanych."
                },
                new()
                {
                    Id = 2,
                    FirstName = "Jacek",
                    MiddleName = "",
                    LastName = "Piekara",
                    Descripiton = "Jeden z najpopularniejszych pisarzy fantastycznych. Prowokujący. Zaskakujący. Potrafi rozbawić, zbulwersować, dotknąć do żywego. Wymyka się oczekiwaniom i nie daje zaszufladkować.\r\nNależał do grup literackich TRUST oraz Klub Tfurcuf."
                }
            });

        }
    }
}
