using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    public partial class AddBudgetScheduleEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetSchedule_Budgets_BudgetID",
                table: "BudgetSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetSchedule",
                table: "BudgetSchedule");

            migrationBuilder.RenameTable(
                name: "BudgetSchedule",
                newName: "BudgetSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetSchedule_BudgetID",
                table: "BudgetSchedules",
                newName: "IX_BudgetSchedules_BudgetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetSchedules",
                table: "BudgetSchedules",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetSchedules_Budgets_BudgetID",
                table: "BudgetSchedules",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetSchedules_Budgets_BudgetID",
                table: "BudgetSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetSchedules",
                table: "BudgetSchedules");

            migrationBuilder.RenameTable(
                name: "BudgetSchedules",
                newName: "BudgetSchedule");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetSchedules_BudgetID",
                table: "BudgetSchedule",
                newName: "IX_BudgetSchedule_BudgetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetSchedule",
                table: "BudgetSchedule",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetSchedule_Budgets_BudgetID",
                table: "BudgetSchedule",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
