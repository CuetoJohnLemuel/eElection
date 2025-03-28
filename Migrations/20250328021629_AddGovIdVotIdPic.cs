using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddGovIdVotIdPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GovernmentId",
                table: "Voters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoterPhotoId",
                table: "Voters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 4,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 6,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 22,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 23,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 24,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "VoterId",
                keyValue: 25,
                columns: new[] { "GovernmentId", "VoterPhotoId" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GovernmentId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "VoterPhotoId",
                table: "Voters");
        }
    }
}
