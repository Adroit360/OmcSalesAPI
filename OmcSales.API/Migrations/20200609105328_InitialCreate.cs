using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OmcSales.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FillingStation",
                columns: table => new
                {
                    FillingStationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillingStation", x => x.FillingStationId);
                });

            migrationBuilder.CreateTable(
                name: "ProductBank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tank",
                columns: table => new
                {
                    TankId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tank", x => x.TankId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    StationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_FillingStation_StationId",
                        column: x => x.StationId,
                        principalTable: "FillingStation",
                        principalColumn: "FillingStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pump",
                columns: table => new
                {
                    PumpId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AttendantName = table.Column<string>(nullable: true),
                    StationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pump", x => x.PumpId);
                    table.ForeignKey(
                        name: "FK_Pump_FillingStation_StationId",
                        column: x => x.StationId,
                        principalTable: "FillingStation",
                        principalColumn: "FillingStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TankValue",
                columns: table => new
                {
                    TankValueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TankId = table.Column<int>(nullable: false),
                    Opening = table.Column<float>(nullable: false),
                    Closing = table.Column<float>(nullable: false),
                    Deliveredproduct = table.Column<float>(nullable: false),
                    RTT = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankValue", x => x.TankValueId);
                    table.ForeignKey(
                        name: "FK_TankValue_Tank_TankId",
                        column: x => x.TankId,
                        principalTable: "Tank",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ProductPriceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.ProductPriceId);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nozzle",
                columns: table => new
                {
                    NozzleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    PumpId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nozzle", x => x.NozzleId);
                    table.ForeignKey(
                        name: "FK_Nozzle_Pump_PumpId",
                        column: x => x.PumpId,
                        principalTable: "Pump",
                        principalColumn: "PumpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NozzleValue",
                columns: table => new
                {
                    NozzleValueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    opening = table.Column<int>(nullable: false),
                    closing = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NozzleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NozzleValue", x => x.NozzleValueId);
                    table.ForeignKey(
                        name: "FK_NozzleValue_Nozzle_NozzleId",
                        column: x => x.NozzleId,
                        principalTable: "Nozzle",
                        principalColumn: "NozzleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nozzle_PumpId",
                table: "Nozzle",
                column: "PumpId");

            migrationBuilder.CreateIndex(
                name: "IX_NozzleValue_NozzleId",
                table: "NozzleValue",
                column: "NozzleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StationId",
                table: "Products",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pump_StationId",
                table: "Pump",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_TankValue_TankId",
                table: "TankValue",
                column: "TankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NozzleValue");

            migrationBuilder.DropTable(
                name: "ProductBank");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "TankValue");

            migrationBuilder.DropTable(
                name: "Nozzle");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tank");

            migrationBuilder.DropTable(
                name: "Pump");

            migrationBuilder.DropTable(
                name: "FillingStation");
        }
    }
}
