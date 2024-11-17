using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Audit.Migrations
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
                name: "CommandAudit",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Command = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueryAudit",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Query = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryAudit", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandAudit",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "QueryAudit",
                schema: "audit");
        }
    }
}
