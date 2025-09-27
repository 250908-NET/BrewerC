using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class AddServerRecordCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordbiIdentity",
                table: "playerNames");

            migrationBuilder.RenameColumn(
                name: "playerRecordbiIdentity",
                table: "playerNames",
                newName: "playerRecordBiIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_playerNames_playerRecordbiIdentity",
                table: "playerNames",
                newName: "IX_playerNames_playerRecordBiIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "serverRecords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_serverRecords_ipAddress_port",
                table: "serverRecords",
                columns: new[] { "ipAddress", "port" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordBiIdentity",
                table: "playerNames",
                column: "playerRecordBiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordBiIdentity",
                table: "playerNames");

            migrationBuilder.DropIndex(
                name: "IX_serverRecords_ipAddress_port",
                table: "serverRecords");

            migrationBuilder.RenameColumn(
                name: "playerRecordBiIdentity",
                table: "playerNames",
                newName: "playerRecordbiIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_playerNames_playerRecordBiIdentity",
                table: "playerNames",
                newName: "IX_playerNames_playerRecordbiIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "serverRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordbiIdentity",
                table: "playerNames",
                column: "playerRecordbiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
