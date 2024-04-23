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
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicles_Garages_GarageId",
                table: "ParkedVehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkedVehicles",
                table: "ParkedVehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Garages",
                table: "Garages");

            migrationBuilder.RenameTable(
                name: "ParkedVehicles",
                newName: "ParkedVehicle");

            migrationBuilder.RenameTable(
                name: "Garages",
                newName: "Garage");

            migrationBuilder.RenameIndex(
                name: "IX_ParkedVehicles_GarageId",
                table: "ParkedVehicle",
                newName: "IX_ParkedVehicle_GarageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Garage",
                table: "Garage",
                column: "ID");

            migrationBuilder.InsertData(
                table: "Garage",
                columns: new[] { "ID", "MaxCapacity", "Name" },
                values: new object[] { 1, 50, "Default Garage One" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Garage_GarageId",
                table: "ParkedVehicle",
                column: "GarageId",
                principalTable: "Garage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Garage_GarageId",
                table: "ParkedVehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Garage",
                table: "Garage");

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Garage",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "ParkedVehicle",
                newName: "ParkedVehicles");

            migrationBuilder.RenameTable(
                name: "Garage",
                newName: "Garages");

            migrationBuilder.RenameIndex(
                name: "IX_ParkedVehicle_GarageId",
                table: "ParkedVehicles",
                newName: "IX_ParkedVehicles_GarageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkedVehicles",
                table: "ParkedVehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Garages",
                table: "Garages",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicles_Garages_GarageId",
                table: "ParkedVehicles",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}