using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    public partial class AddedExpensedAmountOnBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpensedAmount",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensedAmount",
                table: "Budgets");
        }
    }
}
