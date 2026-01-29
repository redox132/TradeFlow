using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _shipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Use conditional SQL to avoid errors if these columns already exist in the database
            migrationBuilder.Sql("ALTER TABLE \"Products\" ADD COLUMN IF NOT EXISTS \"SellerId\" integer NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("ALTER TABLE \"Orders\" ADD COLUMN IF NOT EXISTS \"SellerId\" integer NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("ALTER TABLE \"Orders\" ADD COLUMN IF NOT EXISTS \"UserId\" integer NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("ALTER TABLE \"Customers\" ADD COLUMN IF NOT EXISTS \"SellerId\" integer NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("ALTER TABLE \"Countries\" ADD COLUMN IF NOT EXISTS \"SellerId\" integer NOT NULL DEFAULT 0;");

            migrationBuilder.CreateTable(
                name: "OrderShipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    TrackingNumber = table.Column<string>(type: "text", nullable: false),
                    Carrier = table.Column<string>(type: "text", nullable: false),
                    ShippedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderShipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipments_OrderId",
                table: "OrderShipments",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderShipments");

            // Use conditional SQL to avoid errors if columns were already removed or never added
            migrationBuilder.Sql("ALTER TABLE \"Products\" DROP COLUMN IF EXISTS \"SellerId\";");
            migrationBuilder.Sql("ALTER TABLE \"Orders\" DROP COLUMN IF EXISTS \"SellerId\";");
            migrationBuilder.Sql("ALTER TABLE \"Orders\" DROP COLUMN IF EXISTS \"UserId\";");
            migrationBuilder.Sql("ALTER TABLE \"Customers\" DROP COLUMN IF EXISTS \"SellerId\";");
            migrationBuilder.Sql("ALTER TABLE \"Countries\" DROP COLUMN IF EXISTS \"SellerId\";");
        }
    }
}
