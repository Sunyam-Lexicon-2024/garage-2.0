using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace garage_2._0.Migrations
{
    /// <inheritdoc />
    public partial class ParkingSpotVehicleManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Garage_GarageId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_GarageId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Garage",
                table: "Garage");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Garage",
                newName: "Garages");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Garages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "Garages",
                newName: "ParkingSpotCount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "Vehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(2844),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Garages",
                table: "Garages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageId = table.Column<int>(type: "int", nullable: false),
                    ContainsVehicleType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpots_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpotVehicle",
                columns: table => new
                {
                    ParkingSpotsId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpotVehicle", x => new { x.ParkingSpotsId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ParkingSpotVehicle_ParkingSpots_ParkingSpotsId",
                        column: x => x.ParkingSpotsId,
                        principalTable: "ParkingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingSpotVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ParkingSpotCount" },
                values: new object[] { "Default Garage 1", 5 });

            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "Name", "ParkingSpotCount" },
                values: new object[] { 2, "Default Garage 2", 5 });

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "Id", "ContainsVehicleType", "GarageId" },
                values: new object[,]
                {
                    { 1, 0, 1 },
                    { 2, 0, 1 },
                    { 3, 0, 1 },
                    { 4, 0, 1 },
                    { 5, 0, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(4176));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(4179));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(4181));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(4184));

            migrationBuilder.InsertData(
                table: "ParkingSpotVehicle",
                columns: new[] { "ParkingSpotsId", "VehiclesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "Id", "ContainsVehicleType", "GarageId" },
                values: new object[,]
                {
                    { 6, null, 2 },
                    { 7, null, 2 },
                    { 8, null, 2 },
                    { 9, null, 2 },
                    { 10, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_GarageId",
                table: "ParkingSpots",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpotVehicle_VehiclesId",
                table: "ParkingSpotVehicle",
                column: "VehiclesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpotVehicle");

            migrationBuilder.DropTable(
                name: "ParkingSpots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Garages",
                table: "Garages");

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Garages",
                newName: "Garage");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Garage",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ParkingSpotCount",
                table: "Garage",
                newName: "MaxCapacity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 19, 51, 24, 495, DateTimeKind.Local).AddTicks(2844));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Garage",
                table: "Garage",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "Garage",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "MaxCapacity", "Name" },
                values: new object[] { 50, "Default Garage One" });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8816));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8872));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8875));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 22, 19, 22, 44, 417, DateTimeKind.Local).AddTicks(8878));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_GarageId",
                table: "Vehicle",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Garage_GarageId",
                table: "Vehicle",
                column: "GarageId",
                principalTable: "Garage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
