using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class AddSangunianBayan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MidSanggunianBayan",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MidSanggunianBayan",
                table: "Votes");
        }
    }
}
