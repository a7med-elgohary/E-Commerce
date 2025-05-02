using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commers_Project.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhoto_Users_UserId",
                table: "ProductPhoto");

            migrationBuilder.DropIndex(
                name: "IX_ProductPhoto_UserId",
                table: "ProductPhoto");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductPhoto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductPhoto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhoto_UserId",
                table: "ProductPhoto",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhoto_Users_UserId",
                table: "ProductPhoto",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
