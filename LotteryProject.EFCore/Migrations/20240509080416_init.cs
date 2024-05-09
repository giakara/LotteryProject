using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryProject.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GuestSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LotteryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Guest_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guest_GuestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guest_GuestSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guest_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Present_Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Present_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Present_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Lotteries");

            migrationBuilder.DropTable(
                name: "Presents");
        }
    }
}
