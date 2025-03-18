using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddAllFieldsForMidElec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Votes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MidBarangayCaptainId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidDistrictRepId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidMayorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidPartyListRepId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidProvincialGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidProvincialViceGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidRegionalGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidRegionalViceGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MidSanggunianBarangay",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidViceMayorId",
                table: "Votes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidBarangayCaptainId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidDistrictRepId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidMayorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidPartyListRepId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidProvincialGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidProvincialViceGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidRegionalGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidRegionalViceGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidSanggunianBarangay",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidViceMayorId",
                table: "Votes");
        }
    }
}
