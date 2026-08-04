using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace smsServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 0, "SuperAdmin" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin1" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 2, "Admin2" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 3, "Admin3" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 4, "Admin4" },
                    { new Guid("11111111-1111-1111-1111-111111111116"), new DateTime(2026, 8, 4, 15, 56, 9, 267, DateTimeKind.Utc), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 5, "Admin5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
