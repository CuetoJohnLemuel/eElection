using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddMidSangPanlalawiganToVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MidSangguniangPanlalawigan",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MidSangguniangPanlalawigan",
                table: "Votes");
        }
    }
}
