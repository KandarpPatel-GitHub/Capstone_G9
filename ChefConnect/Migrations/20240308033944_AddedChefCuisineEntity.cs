using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddedChefCuisineEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RecipeImage",
                schema: "Identity",
                table: "ChefRecipes",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateTable(
                name: "ChefCuisines",
                schema: "Identity",
                columns: table => new
                {
                    ChefCuisinesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuisineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChefCuisines", x => x.ChefCuisinesId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefCuisines",
                schema: "Identity");

            migrationBuilder.AlterColumn<byte>(
                name: "RecipeImage",
                schema: "Identity",
                table: "ChefRecipes",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
