using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeAnalytica.DataRegistry.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "measured_quantities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measured_quantities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phys_units",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phys_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sensor_devices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serial_no = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    measured_quantity_id = table.Column<int>(type: "integer", nullable: false),
                    phys_unit_id = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true),
                    installation_date = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    last_maintenance = table.Column<DateTime>(type: "date", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensor_devices", x => x.id);
                    table.ForeignKey(
                        name: "FK_SensorDevice_MeasuredQuantity",
                        column: x => x.measured_quantity_id,
                        principalTable: "measured_quantities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SensorDevice_PhysUnit",
                        column: x => x.phys_unit_id,
                        principalTable: "phys_units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sensor_devices_measured_quantity_id",
                table: "sensor_devices",
                column: "measured_quantity_id");

            migrationBuilder.CreateIndex(
                name: "IX_sensor_devices_phys_unit_id",
                table: "sensor_devices",
                column: "phys_unit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sensor_devices");

            migrationBuilder.DropTable(
                name: "measured_quantities");

            migrationBuilder.DropTable(
                name: "phys_units");
        }
    }
}
