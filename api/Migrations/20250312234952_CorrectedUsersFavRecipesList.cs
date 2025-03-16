using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedUsersFavRecipesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2818317b-c28c-4779-8024-f3cd825d97c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaedb654-11ee-471d-a8cd-77811624a114");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09b110b0-bdf7-469e-8974-1343dfe1f494", null, "User", "USER" },
                    { "acdddf29-3058-4083-b2ee-a524e9e4f45b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09b110b0-bdf7-469e-8974-1343dfe1f494");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acdddf29-3058-4083-b2ee-a524e9e4f45b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2818317b-c28c-4779-8024-f3cd825d97c3", null, "Admin", "ADMIN" },
                    { "eaedb654-11ee-471d-a8cd-77811624a114", null, "User", "USER" }
                });
        }
    }
}
