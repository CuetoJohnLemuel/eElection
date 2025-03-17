using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarangayCaptainId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MayorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ProvincialGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ProvincialViceGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "RegionalAssemblyMembers",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "RegionalGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "RegionalViceGovernorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SKChairmanId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SKMembers",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SangguniangBarangayMembers",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SangguniangPanlalawiganMembers",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SangguniangPanlungsodBayanMembers",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ViceMayorId",
                table: "Votes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarangayCaptainId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Votes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MayorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvincialGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvincialViceGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionalAssemblyMembers",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RegionalGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionalViceGovernorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SKChairmanId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKMembers",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SangguniangBarangayMembers",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SangguniangPanlalawiganMembers",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SangguniangPanlungsodBayanMembers",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViceMayorId",
                table: "Votes",
                type: "int",
                nullable: true);
        }
    }
}
