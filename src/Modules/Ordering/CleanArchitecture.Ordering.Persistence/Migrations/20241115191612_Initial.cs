using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Ordering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "ordering",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BrokerId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CommodityId = table.Column<int>(type: "int", nullable: false),
                    CommodityName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    InsertCommandCorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateCommandCorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "ordering");
        }
    }
}
