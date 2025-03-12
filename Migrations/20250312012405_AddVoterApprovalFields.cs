using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddVoterApprovalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Voters_VoterID",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_VoterID",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "VoterID",
                table: "Account",
                newName: "VoterId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Birthdate",
                table: "Voters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Voters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Voters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Voters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Account_VoterId",
                table: "Account",
                column: "VoterId",
                unique: true,
                filter: "[VoterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Voters_VoterId",
                table: "Account",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "VoterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Voters_VoterId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_VoterId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Voters");

            migrationBuilder.RenameColumn(
                name: "VoterId",
                table: "Account",
                newName: "VoterID");

            migrationBuilder.CreateIndex(
                name: "IX_Account_VoterID",
                table: "Account",
                column: "VoterID",
                unique: true,
                filter: "[VoterID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Voters_VoterID",
                table: "Account",
                column: "VoterID",
                principalTable: "Voters",
                principalColumn: "VoterId");
        }
    }
}
