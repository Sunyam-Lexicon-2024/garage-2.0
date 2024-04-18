using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace garage_2._0.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
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

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 25, 27, 433, DateTimeKind.Local).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 25, 27, 433, DateTimeKind.Local).AddTicks(3159));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 25, 27, 433, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 25, 27, 433, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 25, 27, 433, DateTimeKind.Local).AddTicks(3167));

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

            migrationBuilder.UpdateData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7696));

            migrationBuilder.UpdateData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7699));

            migrationBuilder.UpdateData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "ParkedVehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegisteredAt",
                value: new DateTime(2024, 4, 18, 18, 12, 51, 530, DateTimeKind.Local).AddTicks(7704));

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
