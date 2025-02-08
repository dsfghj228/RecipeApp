using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UserRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e243b87-f164-4edf-b1de-287df0b951ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dda33f01-972b-4bfb-981c-18d2b8c54a31");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ad5eddf-663d-4ea2-b569-c6ffc909cc23", null, "User", "USER" },
                    { "8bda73a0-01f5-49c3-a48e-eaa65c0fbab6", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ad5eddf-663d-4ea2-b569-c6ffc909cc23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bda73a0-01f5-49c3-a48e-eaa65c0fbab6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e243b87-f164-4edf-b1de-287df0b951ca", null, "User", "USER" },
                    { "dda33f01-972b-4bfb-981c-18d2b8c54a31", null, "Admin", "ADMIN" }
                });
        }
    }
}
