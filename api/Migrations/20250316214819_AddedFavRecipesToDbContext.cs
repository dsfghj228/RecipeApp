using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddedFavRecipesToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60548537-de07-48f9-8cee-70749c853f92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcabf37c-f8ff-4078-a39f-d8ba6275e870");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipe",
                newName: "FavoriteRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipe_UserId",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes",
                columns: new[] { "RecipeId", "UserId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a7d5198-0856-499b-b9e5-d3c3ba3e640f", null, "Admin", "ADMIN" },
                    { "d5dd107e-da1a-4ac4-961d-71d0610024ee", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserId",
                table: "FavoriteRecipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeId",
                table: "FavoriteRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserId",
                table: "FavoriteRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeId",
                table: "FavoriteRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a7d5198-0856-499b-b9e5-d3c3ba3e640f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5dd107e-da1a-4ac4-961d-71d0610024ee");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipes",
                newName: "FavoriteRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserId",
                table: "FavoriteRecipe",
                newName: "IX_FavoriteRecipe_UserId");

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
    }
}
