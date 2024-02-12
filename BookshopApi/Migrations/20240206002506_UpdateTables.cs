using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookshopApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BooksAuthors",
                keyColumns: new[] { "author_id", "book_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BooksAuthors",
                keyColumns: new[] { "author_id", "book_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.RenameColumn(
                name: "PublisherName",
                table: "Publishers",
                newName: "publisher_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Publishers",
                newName: "publisher_id");

            migrationBuilder.AlterColumn<string>(
                name: "publisher_name",
                table: "Publishers",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publisher_name",
                table: "Publishers",
                newName: "PublisherName");

            migrationBuilder.RenameColumn(
                name: "publisher_id",
                table: "Publishers",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40);

            migrationBuilder.InsertData(
                table: "BooksAuthors",
                columns: new[] { "author_id", "book_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }
    }
}
