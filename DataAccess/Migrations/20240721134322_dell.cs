using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_PrivilegedHalls_PrivilegedHallId",
                table: "MovieTheaters");

            migrationBuilder.DropTable(
                name: "PrivilegedHalls");

            migrationBuilder.DropIndex(
                name: "IX_MovieTheaters_PrivilegedHallId",
                table: "MovieTheaters");

            migrationBuilder.DropColumn(
                name: "PrivilegedHallId",
                table: "MovieTheaters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivilegedHallId",
                table: "MovieTheaters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrivilegedHalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Picture = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SummaryDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegedHalls", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTheaters_PrivilegedHallId",
                table: "MovieTheaters",
                column: "PrivilegedHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_PrivilegedHalls_PrivilegedHallId",
                table: "MovieTheaters",
                column: "PrivilegedHallId",
                principalTable: "PrivilegedHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
