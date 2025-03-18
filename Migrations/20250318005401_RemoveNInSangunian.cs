using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNInSangunian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MidSangguniangPanlalawigan",
                table: "Votes",
                newName: "MidSanggunianPanlalawigan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MidSanggunianPanlalawigan",
                table: "Votes",
                newName: "MidSangguniangPanlalawigan");
        }
    }
}
