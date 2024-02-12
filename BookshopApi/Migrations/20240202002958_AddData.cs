using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookshopApi.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "author_id", "description", "first_name", "last_name", "middle_name" },
                values: new object[,]
                {
                    { 1, "Prawnik, filozof, pisarz społeczny i polityczny, historyk i dyplomata florencki, jeden z najwybitniejszych przedstawicieli renesansowej myśli politycznej. Napisał traktat o sprawowaniu władzy pt. Książę, który sprawił, że od jego nazwiska powstał termin makiawelizm. Opisywał funkcjonowanie zarówno republik, jak i królestw. W 1559 jego pisma znalazły się na kościelnym indeksie ksiąg zakazanych.", "Mikołaj", "Machiavelli", "" },
                    { 2, "Jeden z najpopularniejszych pisarzy fantastycznych. Prowokujący. Zaskakujący. Potrafi rozbawić, zbulwersować, dotknąć do żywego. Wymyka się oczekiwaniom i nie daje zaszufladkować.\r\nNależał do grup literackich TRUST oraz Klub Tfurcuf.", "Jacek", "Piekara", "" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "PublisherName" },
                values: new object[,]
                {
                    { 1, "PWN" },
                    { 2, "Runa" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "book_id", "description", "language", "pages", "photo_url", "price", "published_date", "pub_id", "rate", "series", "title" },
                values: new object[,]
                {
                    { 1, "Książę", "Polski", 300, "http://Ksiaze.com", 22.22m, new DateTime(2020, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5.5, "", "Książę" },
                    { 2, "Ani słowa prawdy", "Polski", 300, "http://Anislowaprawdy.com", 33.22m, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5.5, "", "Ani słowa prawdy" }
                });

            migrationBuilder.InsertData(
                table: "BooksAuthors",
                columns: new[] { "author_id", "book_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BooksAuthors",
                keyColumns: new[] { "author_id", "book_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BooksAuthors",
                keyColumns: new[] { "author_id", "book_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "author_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "author_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
