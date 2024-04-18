using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace garage_2._0.Migrations
{
    /// <inheritdoc />
    public partial class AddGarageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GarageId",
                table: "ParkedVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicles_GarageId",
                table: "ParkedVehicles",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicles_Garages_GarageId",
                table: "ParkedVehicles",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicles_Garages_GarageId",
                table: "ParkedVehicles");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicles_GarageId",
                table: "ParkedVehicles");

            migrationBuilder.DropColumn(
                name: "GarageId",
                table: "ParkedVehicles");
        }
    }
}
