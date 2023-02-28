using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Persistance.Migrations
{
    public partial class AddActivePropEventQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "Active",
                table: "EventQuestions",
                type: "tinyint",
                nullable: false,
                defaultValue: (sbyte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "EventQuestions");
        }
    }
}
