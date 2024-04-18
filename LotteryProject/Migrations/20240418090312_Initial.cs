using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuestSurname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PresentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LotteryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotteries_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lotteries_Presents_PresentId",
                        column: x => x.PresentId,
                        principalTable: "Presents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotteries_GuestId",
                table: "Lotteries",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Lotteries_PresentId",
                table: "Lotteries",
                column: "PresentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotteries");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Presents");
        }
    }
}
