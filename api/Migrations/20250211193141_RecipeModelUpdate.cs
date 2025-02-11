using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RecipeModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ad5eddf-663d-4ea2-b569-c6ffc909cc23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bda73a0-01f5-49c3-a48e-eaa65c0fbab6");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Recipes",
                newName: "PhotoName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b47daff2-0d8e-43e0-b48f-80a584d3a678", null, "Admin", "ADMIN" },
                    { "cfd4ad99-7fc5-41b6-97c2-04a05a0545f6", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b47daff2-0d8e-43e0-b48f-80a584d3a678");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfd4ad99-7fc5-41b6-97c2-04a05a0545f6");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Recipes",
                newName: "PhotoUrl");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ad5eddf-663d-4ea2-b569-c6ffc909cc23", null, "User", "USER" },
                    { "8bda73a0-01f5-49c3-a48e-eaa65c0fbab6", null, "Admin", "ADMIN" }
                });
        }
    }
}
