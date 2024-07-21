using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qate3DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Prod_Title",
                table: "Products",
                column: "Prod_Title");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Dept_Title",
                table: "Departments",
                column: "Dept_Title");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Cat_Title",
                table: "Categories",
                column: "Cat_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Prod_Title",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Dept_Title",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Cat_Title",
                table: "Categories");
        }
    }
}
