using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class UserManagement_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "um");

            migrationBuilder.CreateTable(
                name: "OneTimePassword",
                schema: "um",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimePassword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OneTimeToken",
                schema: "um",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                schema: "um",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginMethod = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "um",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Passsword = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "um",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "um",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneTimePassword",
                schema: "um");

            migrationBuilder.DropTable(
                name: "OneTimeToken",
                schema: "um");

            migrationBuilder.DropTable(
                name: "Session",
                schema: "um");

            migrationBuilder.DropTable(
                name: "User",
                schema: "um");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "um");
        }
    }
}
