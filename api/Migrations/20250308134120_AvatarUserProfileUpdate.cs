using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AvatarUserProfileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b47daff2-0d8e-43e0-b48f-80a584d3a678");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfd4ad99-7fc5-41b6-97c2-04a05a0545f6");

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4bd610da-f083-48b4-9f02-a64a355c250c", null, "Admin", "ADMIN" },
                    { "faae6bb9-888b-4b38-adb1-c20c4d5a6cf6", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bd610da-f083-48b4-9f02-a64a355c250c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faae6bb9-888b-4b38-adb1-c20c4d5a6cf6");

            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b47daff2-0d8e-43e0-b48f-80a584d3a678", null, "Admin", "ADMIN" },
                    { "cfd4ad99-7fc5-41b6-97c2-04a05a0545f6", null, "User", "USER" }
                });
        }
    }
}
