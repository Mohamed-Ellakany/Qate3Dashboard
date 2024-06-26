using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qate3DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_SubCategory");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_categoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Category_SubCategory",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    subCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_SubCategory", x => new { x.categoryId, x.subCategoryId });
                    table.ForeignKey(
                        name: "FK_Category_SubCategory_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Category_SubCategory_SubCategory_subCategoryId",
                        column: x => x.subCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_SubCategory_subCategoryId",
                table: "Category_SubCategory",
                column: "subCategoryId");
        }
    }
}
