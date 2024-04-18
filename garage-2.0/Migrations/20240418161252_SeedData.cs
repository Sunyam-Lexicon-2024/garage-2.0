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
            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "ID", "MaxCapacity", "Name" },
                values: new object[] { 1, 50, "Default Garage One" });

            migrationBuilder.InsertData(
                table: "ParkedVehicles",
                columns: new[] { "Id", "Brand", "Color", "GarageId", "Model", "RegisteredAt", "RegistrationNumber", "Type", "Wheels" },
                values: new object[,]
                {
                    { 1, "Volkswagen", 4, 1, "Unknown", new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7634), "FPD941", 0, 4 },
                    { 2, "Saab", 1, 1, "Unknown", new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7696), "CLQ415", 0, 4 },
                    { 3, "Volvo", 2, 1, "Unknown", new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7699), "YHV901", 0, 4 },
                    { 4, "Audi", 7, 1, "Unknown", new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7701), "GBO781", 0, 4 },
                    { 5, "Toyota", 0, 1, "Unknown", new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7704), "JRC132", 0, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
