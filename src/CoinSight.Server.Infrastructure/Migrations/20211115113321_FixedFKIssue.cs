using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    public partial class FixedFKIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Budgets_BudgetID",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Users_UserUUID",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_BudgetID",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserUUID",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "UserUUID",
                table: "Reminders");

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetedAmount",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetedAmount",
                table: "Budgets");

            migrationBuilder.AddColumn<Guid>(
                name: "UserUUID",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_BudgetID",
                table: "Reminders",
                column: "BudgetID");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserUUID",
                table: "Reminders",
                column: "UserUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Budgets_BudgetID",
                table: "Reminders",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Users_UserUUID",
                table: "Reminders",
                column: "UserUUID",
                principalTable: "Users",
                principalColumn: "UUID");
        }
    }
}
