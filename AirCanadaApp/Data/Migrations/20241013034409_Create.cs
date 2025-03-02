using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirCanadaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightData",
                columns: table => new
                {
                    FlightDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightData", x => x.FlightDataId);
                });

            migrationBuilder.CreateTable(
                name: "TicketOrder",
                columns: table => new
                {
                    TicketOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccessible = table.Column<bool>(type: "bit", nullable: false),
                    SpecialInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    FlightDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrder", x => x.TicketOrderId);
                    table.ForeignKey(
                        name: "FK_TicketOrder_FlightData_FlightDataId",
                        column: x => x.FlightDataId,
                        principalTable: "FlightData",
                        principalColumn: "FlightDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrder_FlightDataId",
                table: "TicketOrder",
                column: "FlightDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrder");

            migrationBuilder.DropTable(
                name: "FlightData");
        }
    }
}
