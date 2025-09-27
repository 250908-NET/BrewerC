using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerIpAddressCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordbiIdentity",
                table: "playerIpAddresses");

            migrationBuilder.DropIndex(
                name: "IX_playerIpAddresses_playerRecordbiIdentity",
                table: "playerIpAddresses");

            migrationBuilder.RenameColumn(
                name: "playerRecordbiIdentity",
                table: "playerIpAddresses",
                newName: "playerRecordBiIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "playerIpAddresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_playerIpAddresses_playerRecordBiIdentity_ipAddress",
                table: "playerIpAddresses",
                columns: new[] { "playerRecordBiIdentity", "ipAddress" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordBiIdentity",
                table: "playerIpAddresses",
                column: "playerRecordBiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordBiIdentity",
                table: "playerIpAddresses");

            migrationBuilder.DropIndex(
                name: "IX_playerIpAddresses_playerRecordBiIdentity_ipAddress",
                table: "playerIpAddresses");

            migrationBuilder.RenameColumn(
                name: "playerRecordBiIdentity",
                table: "playerIpAddresses",
                newName: "playerRecordbiIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "playerIpAddresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_playerIpAddresses_playerRecordbiIdentity",
                table: "playerIpAddresses",
                column: "playerRecordbiIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordbiIdentity",
                table: "playerIpAddresses",
                column: "playerRecordbiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
