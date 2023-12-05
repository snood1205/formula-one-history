using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOneHistory.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalTeamId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalRaceId",
                table: "Races",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalRaceResultId",
                table: "RaceResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalDriverId",
                table: "Drivers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalTeamId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ExternalRaceId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "ExternalRaceResultId",
                table: "RaceResults");

            migrationBuilder.DropColumn(
                name: "ExternalDriverId",
                table: "Drivers");
        }
    }
}
