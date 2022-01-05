using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    public partial class RemovedUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserUUID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Budgets_BudgetID",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserUUID",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserUUID",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetID",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetID",
                table: "Expenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetID",
                table: "Expenses",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Budgets_BudgetID",
                table: "Notes",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Budgets_BudgetID",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetID",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BudgetID",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUUID",
                table: "Budgets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UUID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserUUID",
                table: "Budgets",
                column: "UserUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserUUID",
                table: "Budgets",
                column: "UserUUID",
                principalTable: "Users",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetID",
                table: "Expenses",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Budgets_BudgetID",
                table: "Notes",
                column: "BudgetID",
                principalTable: "Budgets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
