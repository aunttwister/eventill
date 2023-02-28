using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.Persistance.Migrations
{
    public partial class InitialCreateExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventQuestion_Events_EventId",
                table: "EventQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_EventQuestion_Question_QuestionId",
                table: "EventQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_EventOccurence_EventOccurenceId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Reservations_ReservationId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketExtension_Ticket_TicketId",
                table: "TicketExtension");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EventOccurence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventQuestion",
                table: "EventQuestion");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "EventQuestion",
                newName: "EventQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_ReservationId",
                table: "Tickets",
                newName: "IX_Tickets_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_EventOccurenceId",
                table: "Tickets",
                newName: "IX_Tickets_EventOccurenceId");

            migrationBuilder.RenameIndex(
                name: "IX_EventQuestion_QuestionId",
                table: "EventQuestions",
                newName: "IX_EventQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_EventQuestion_EventId",
                table: "EventQuestions",
                newName: "IX_EventQuestions_EventId");

            migrationBuilder.AlterColumn<long>(
                name: "ReservationId",
                table: "Tickets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventQuestions",
                table: "EventQuestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventOccurrences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOccurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventOccurrences_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_EventId",
                table: "EventOccurrences",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventQuestions_Events_EventId",
                table: "EventQuestions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventQuestions_Questions_QuestionId",
                table: "EventQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketExtension_Tickets_TicketId",
                table: "TicketExtension",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_EventOccurrences_EventOccurenceId",
                table: "Tickets",
                column: "EventOccurenceId",
                principalTable: "EventOccurrences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Reservations_ReservationId",
                table: "Tickets",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventQuestions_Events_EventId",
                table: "EventQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_EventQuestions_Questions_QuestionId",
                table: "EventQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketExtension_Tickets_TicketId",
                table: "TicketExtension");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_EventOccurrences_EventOccurenceId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Reservations_ReservationId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EventOccurrences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventQuestions",
                table: "EventQuestions");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "EventQuestions",
                newName: "EventQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ReservationId",
                table: "Ticket",
                newName: "IX_Ticket_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_EventOccurenceId",
                table: "Ticket",
                newName: "IX_Ticket_EventOccurenceId");

            migrationBuilder.RenameIndex(
                name: "IX_EventQuestions_QuestionId",
                table: "EventQuestion",
                newName: "IX_EventQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_EventQuestions_EventId",
                table: "EventQuestion",
                newName: "IX_EventQuestion_EventId");

            migrationBuilder.AlterColumn<long>(
                name: "ReservationId",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventQuestion",
                table: "EventQuestion",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventOccurence",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOccurence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventOccurence_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurence_EventId",
                table: "EventOccurence",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventQuestion_Events_EventId",
                table: "EventQuestion",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventQuestion_Question_QuestionId",
                table: "EventQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_EventOccurence_EventOccurenceId",
                table: "Ticket",
                column: "EventOccurenceId",
                principalTable: "EventOccurence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Reservations_ReservationId",
                table: "Ticket",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketExtension_Ticket_TicketId",
                table: "TicketExtension",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
