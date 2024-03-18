using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefConnect.Migrations
{
    /// <inheritdoc />
    public partial class updatedCuisineAndChefRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChefRecipes_CuisineId",
                schema: "Identity",
                table: "ChefRecipes",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChefRecipes_Cuisines_CuisineId",
                schema: "Identity",
                table: "ChefRecipes",
                column: "CuisineId",
                principalSchema: "Identity",
                principalTable: "Cuisines",
                principalColumn: "CuisinesId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChefRecipes_Cuisines_CuisineId",
                schema: "Identity",
                table: "ChefRecipes");

            migrationBuilder.DropIndex(
                name: "IX_ChefRecipes_CuisineId",
                schema: "Identity",
                table: "ChefRecipes");
        }
    }
}
