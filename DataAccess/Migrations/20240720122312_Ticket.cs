using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SellTime",
                table: "Tickets",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WorkerId",
                table: "Tickets",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_WorkerId",
                table: "Tickets",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_WorkerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_WorkerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Tickets");
        }
    }
}
