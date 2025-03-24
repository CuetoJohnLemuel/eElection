using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnMaxCandidates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 25);

            migrationBuilder.AddColumn<int>(
                name: "MaxCandidates",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 6,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 7,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 8,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 9,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 10,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 11,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 12,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 14,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 15,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 16,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 17,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 18,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 19,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 20,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 21,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 22,
                column: "MaxCandidates",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "PositionId",
                keyValue: 23,
                column: "MaxCandidates",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCandidates",
                table: "Positions");

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "CreatedAt", "PositionName" },
                values: new object[,]
                {
                    { 13, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Regional Assembly Members" },
                    { 24, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Councilors for Indigenous Peoples (IPs)" },
                    { 25, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Councilors for Sectoral Representatives" }
                });
        }
    }
}
