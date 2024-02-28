using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefConnect.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderInstructions",
                schema: "Identity",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                schema: "Identity",
                table: "ChefRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "Identity",
                table: "ChefRecipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PricePerExtraPerson",
                schema: "Identity",
                table: "ChefRecipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderInstructions",
                schema: "Identity",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfPeople",
                schema: "Identity",
                table: "ChefRecipes");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Identity",
                table: "ChefRecipes");

            migrationBuilder.DropColumn(
                name: "PricePerExtraPerson",
                schema: "Identity",
                table: "ChefRecipes");
        }
    }
}
