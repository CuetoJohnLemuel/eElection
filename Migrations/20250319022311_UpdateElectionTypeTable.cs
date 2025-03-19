using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class UpdateElectionTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionTypes_Elections_ElectionId",
                table: "ElectionTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionTypes_Positions_PositionId",
                table: "ElectionTypes");

            migrationBuilder.DropIndex(
                name: "IX_ElectionTypes_ElectionId",
                table: "ElectionTypes");

            migrationBuilder.DropIndex(
                name: "IX_ElectionTypes_PositionId",
                table: "ElectionTypes");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "ElectionTypes");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "ElectionTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "ElectionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "ElectionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionTypes_ElectionId",
                table: "ElectionTypes",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionTypes_PositionId",
                table: "ElectionTypes",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionTypes_Elections_ElectionId",
                table: "ElectionTypes",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionTypes_Positions_PositionId",
                table: "ElectionTypes",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
