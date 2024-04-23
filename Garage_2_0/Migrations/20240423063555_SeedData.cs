using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace garage_2._0.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicles");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.CreateTable(
                name: "Garage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wheels = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    GarageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkedVehicle_Garage_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Garage",
                columns: new[] { "Id", "MaxCapacity", "Name" },
                values: new object[,]
                {
                    { 1, 50, "Garage One" },
                    { 2, 100, "Garage Two" },
                    { 3, 25, "Garage Three" }
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "Brand", "Color", "GarageId", "Model", "RegisteredAt", "RegistrationNumber", "Type", "Wheels" },
                values: new object[,]
                {
                    { 1, "Volkswagen", 4, 1, "Unknown", new DateTime(2024, 4, 23, 8, 35, 53, 283, DateTimeKind.Local).AddTicks(1288), "FPD941", 0, 4 },
                    { 2, "Saab", 1, 1, "Unknown", new DateTime(2024, 4, 23, 8, 35, 53, 283, DateTimeKind.Local).AddTicks(1359), "CLQ415", 0, 4 },
                    { 3, "Volvo", 2, 1, "Unknown", new DateTime(2024, 4, 23, 8, 35, 53, 283, DateTimeKind.Local).AddTicks(1364), "YHV901", 0, 4 },
                    { 4, "Audi", 7, 1, "Unknown", new DateTime(2024, 4, 23, 8, 35, 53, 283, DateTimeKind.Local).AddTicks(1367), "GBO781", 0, 4 },
                    { 5, "Toyota", 0, 1, "Unknown", new DateTime(2024, 4, 23, 8, 35, 53, 283, DateTimeKind.Local).AddTicks(1371), "JRC132", 0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_GarageId",
                table: "ParkedVehicle",
                column: "GarageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "Garage");

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkedVehicles",
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
                    table.PrimaryKey("PK_ParkedVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkedVehicles_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicles_GarageId",
                table: "ParkedVehicles",
                column: "GarageId");
        }
    }
}
