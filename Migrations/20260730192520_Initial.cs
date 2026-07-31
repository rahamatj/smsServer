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
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 0, "SuperAdmin" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin1" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin2" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin3" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin4" },
                    { new Guid("11111111-1111-1111-1111-111111111116"), "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC", null, null, 1, "Admin5" }
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
