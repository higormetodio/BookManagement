using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BooksStockAmoutBorrowed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "INT",
                table: "BookStock",
                newName: "AmoutBorrowed");

            migrationBuilder.AlterColumn<int>(
                name: "AmoutBorrowed",
                table: "BookStock",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmoutBorrowed",
                table: "BookStock",
                newName: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "INT",
                table: "BookStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
