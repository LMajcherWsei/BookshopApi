using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookshopApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToSecondDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10247a8f-17a1-42ff-9139-1ac97bc5be5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e36d18cd-898e-4fca-b0f9-7a6e8c1402fc");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d5d57b0-6a06-4cd2-b3a9-1e163d6ea807", null, "User", "USER" },
                    { "43bb4375-a793-4f26-bc91-a2a819bf4046", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1,
                column: "category",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 2,
                column: "category",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d5d57b0-6a06-4cd2-b3a9-1e163d6ea807");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43bb4375-a793-4f26-bc91-a2a819bf4046");

            migrationBuilder.DropColumn(
                name: "category",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10247a8f-17a1-42ff-9139-1ac97bc5be5b", null, "User", "USER" },
                    { "e36d18cd-898e-4fca-b0f9-7a6e8c1402fc", null, "Admin", "ADMIN" }
                });
        }
    }
}
