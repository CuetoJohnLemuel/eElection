using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToVoter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_VoterId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Voters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Voters",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_AccountId",
                table: "Voters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_VoterId",
                table: "Account",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_Account_AccountId",
                table: "Voters",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_Account_AccountId",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Voters_AccountId",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Account_VoterId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Voters");

            migrationBuilder.CreateIndex(
                name: "IX_Account_VoterId",
                table: "Account",
                column: "VoterId",
                unique: true,
                filter: "[VoterId] IS NOT NULL");
        }
    }
}
