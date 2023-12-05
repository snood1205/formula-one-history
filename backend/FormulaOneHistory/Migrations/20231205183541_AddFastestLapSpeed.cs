using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOneHistory.Migrations
{
    /// <inheritdoc />
    public partial class AddFastestLapSpeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FastestLapSpeed",
                table: "RaceResults",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FastestLapSpeed",
                table: "RaceResults");
        }
    }
}
