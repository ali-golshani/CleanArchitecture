using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.RequestAudit.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests",
                schema: "audit");
        }
    }
}
