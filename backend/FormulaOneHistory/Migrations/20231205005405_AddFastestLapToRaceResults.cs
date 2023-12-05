using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOneHistory.Migrations
{
    /// <inheritdoc />
    public partial class AddFastestLapToRaceResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FastestLap",
                table: "RaceResults",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FastestLap",
                table: "RaceResults");
        }
    }
}
