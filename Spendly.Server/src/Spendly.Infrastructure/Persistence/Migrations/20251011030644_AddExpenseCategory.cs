using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spendly.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseExpensesCategory_ExpensesCategory_CategoryId",
                table: "ExpenseExpensesCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpensesCategory",
                table: "ExpensesCategory");

            migrationBuilder.RenameTable(
                name: "ExpensesCategory",
                newName: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "CustomCategory",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseExpensesCategory_Categories_CategoryId",
                table: "ExpenseExpensesCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseExpensesCategory_Categories_CategoryId",
                table: "ExpenseExpensesCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CustomCategory",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "ExpensesCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpensesCategory",
                table: "ExpensesCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseExpensesCategory_ExpensesCategory_CategoryId",
                table: "ExpenseExpensesCategory",
                column: "CategoryId",
                principalTable: "ExpensesCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
