using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace garage_2._0.Migrations
{
    /// <inheritdoc />
    public partial class ChangeParkedVehicleToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicle");

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageId = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wheels = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Garage_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "Brand", "Color", "GarageId", "Model", "RegisteredAt", "RegistrationNumber", "Type", "Wheels" },
                values: new object[,]
                {
                    { 1, "Volkswagen", 4, 1, "Unknown", new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8816), "FPD941", 0, 4 },
                    { 2, "Saab", 1, 1, "Unknown", new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8869), "CLQ415", 0, 4 },
                    { 3, "Volvo", 2, 1, "Unknown", new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8872), "YHV901", 0, 4 },
                    { 4, "Audi", 7, 1, "Unknown", new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8875), "GBO781", 0, 4 },
                    { 5, "Toyota", 0, 1, "Unknown", new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8878), "JRC132", 0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_GarageId",
                table: "Vehicle",
                column: "GarageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageId = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Wheels = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkedVehicle_Garage_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "Brand", "Color", "GarageId", "Model", "RegisteredAt", "RegistrationNumber", "Type", "Wheels" },
                values: new object[,]
                {
                    { 1, "Volkswagen", 4, 1, "Unknown", new DateTime(2024, 4, 19, 11, 22, 35, 539, DateTimeKind.Local).AddTicks(2992), "FPD941", 0, 4 },
                    { 2, "Saab", 1, 1, "Unknown", new DateTime(2024, 4, 19, 11, 22, 35, 539, DateTimeKind.Local).AddTicks(3048), "CLQ415", 0, 4 },
                    { 3, "Volvo", 2, 1, "Unknown", new DateTime(2024, 4, 19, 11, 22, 35, 539, DateTimeKind.Local).AddTicks(3051), "YHV901", 0, 4 },
                    { 4, "Audi", 7, 1, "Unknown", new DateTime(2024, 4, 19, 11, 22, 35, 539, DateTimeKind.Local).AddTicks(3054), "GBO781", 0, 4 },
                    { 5, "Toyota", 0, 1, "Unknown", new DateTime(2024, 4, 19, 11, 22, 35, 539, DateTimeKind.Local).AddTicks(3056), "JRC132", 0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_GarageId",
                table: "ParkedVehicle",
                column: "GarageId");
        }
    }
}
