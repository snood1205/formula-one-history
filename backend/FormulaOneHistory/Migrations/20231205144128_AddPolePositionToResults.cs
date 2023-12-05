﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOneHistory.Migrations
{
    /// <inheritdoc />
    public partial class AddPolePositionToResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PolePosition",
                table: "RaceResults",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolePosition",
                table: "RaceResults");
        }
    }
}
