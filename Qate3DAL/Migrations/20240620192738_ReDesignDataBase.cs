using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qate3DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReDesignDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoyCutOrNot",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategory_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategory_SubcategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "BoyCutOrNot",
                table: "Products",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
