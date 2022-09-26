using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleApp.Infrastructure.Migrations
{
    public partial class FixIdProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrderItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
