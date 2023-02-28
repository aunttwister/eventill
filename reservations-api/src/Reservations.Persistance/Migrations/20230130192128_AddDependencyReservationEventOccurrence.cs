using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Persistance.Migrations
{
    public partial class AddDependencyReservationEventOccurrence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventOccurrenceId",
                table: "Reservations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventOccurrenceId",
                table: "Reservations",
                column: "EventOccurrenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_EventOccurrences_EventOccurrenceId",
                table: "Reservations",
                column: "EventOccurrenceId",
                principalTable: "EventOccurrences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_EventOccurrences_EventOccurrenceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EventOccurrenceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EventOccurrenceId",
                table: "Reservations");
        }
    }
}
