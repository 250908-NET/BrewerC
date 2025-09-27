using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerNameCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_playerNames_playerRecordBiIdentity",
                table: "playerNames");

            migrationBuilder.CreateIndex(
                name: "IX_playerNames_playerRecordBiIdentity_name",
                table: "playerNames",
                columns: new[] { "playerRecordBiIdentity", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_playerNames_playerRecordBiIdentity_name",
                table: "playerNames");

            migrationBuilder.CreateIndex(
                name: "IX_playerNames_playerRecordBiIdentity",
                table: "playerNames",
                column: "playerRecordBiIdentity");
        }
    }
}
