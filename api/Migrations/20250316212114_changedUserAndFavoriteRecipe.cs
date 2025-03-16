using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class changedUserAndFavoriteRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_AppUserId",
                table: "FavoriteRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteRecipe_AppUserId",
                table: "FavoriteRecipe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09b110b0-bdf7-469e-8974-1343dfe1f494");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acdddf29-3058-4083-b2ee-a524e9e4f45b");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FavoriteRecipe");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FavoriteRecipe");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "FavoriteRecipe",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FavoriteRecipe",
                newName: "RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe",
                columns: new[] { "RecipeId", "UserId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60548537-de07-48f9-8cee-70749c853f92", null, "Admin", "ADMIN" },
                    { "fcabf37c-f8ff-4078-a39f-d8ba6275e870", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRecipe_UserId",
                table: "FavoriteRecipe",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserId",
                table: "FavoriteRecipe",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeId",
                table: "FavoriteRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserId",
                table: "FavoriteRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeId",
                table: "FavoriteRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteRecipe_UserId",
                table: "FavoriteRecipe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60548537-de07-48f9-8cee-70749c853f92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcabf37c-f8ff-4078-a39f-d8ba6275e870");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteRecipe",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "FavoriteRecipe",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FavoriteRecipe",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FavoriteRecipe",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09b110b0-bdf7-469e-8974-1343dfe1f494", null, "User", "USER" },
                    { "acdddf29-3058-4083-b2ee-a524e9e4f45b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRecipe_AppUserId",
                table: "FavoriteRecipe",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_AppUserId",
                table: "FavoriteRecipe",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
