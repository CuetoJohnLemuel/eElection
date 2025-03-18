using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDifferentColumnInVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictRepId",
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
                name: "MidSanggunianBayan",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidSanggunianPanlalawigan",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidSenators",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MidViceMayorId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PartyListRepId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PresidentId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Senators",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VicePresidentId",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "DistrictRepId",
                table: "Votes",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "MidSanggunianBayan",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MidSanggunianPanlalawigan",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MidSenators",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MidViceMayorId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyListRepId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresidentId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senators",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VicePresidentId",
                table: "Votes",
                type: "int",
                nullable: true);
        }
    }
}
