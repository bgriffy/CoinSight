using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    public partial class AddPaymentURLColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentURL",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentURL",
                table: "Expenses");
        }
    }
}
