using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddElectionIdInVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Votes");
        }
    }
}
