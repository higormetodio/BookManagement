using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BooksStockAmoutBorrowed3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmoutBorrowed",
                table: "BookStock",
                newName: "LoanQuantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanQuantity",
                table: "BookStock",
                newName: "AmoutBorrowed");
        }
    }
}
