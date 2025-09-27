using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModelsAndColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordBiIdentity",
                table: "playerIpAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordBiIdentity",
                table: "playerNames");

            migrationBuilder.DropForeignKey(
                name: "FK_serverPlayerConnections_playerRecords_playerRecordbiIdentity",
                table: "serverPlayerConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_serverPlayerConnections_serverRecords_serverRecordid",
                table: "serverPlayerConnections");

            migrationBuilder.RenameColumn(
                name: "ipAddress",
                table: "serverRecords",
                newName: "ip_address");

            migrationBuilder.RenameIndex(
                name: "IX_serverRecords_ipAddress_port",
                table: "serverRecords",
                newName: "IX_serverRecords_ip_address_port");

            migrationBuilder.RenameColumn(
                name: "connectionTime",
                table: "serverPlayerConnections",
                newName: "connection_time");

            migrationBuilder.RenameColumn(
                name: "serverRecordid",
                table: "serverPlayerConnections",
                newName: "server_id");

            migrationBuilder.RenameColumn(
                name: "playerRecordbiIdentity",
                table: "serverPlayerConnections",
                newName: "bi_identity");

            migrationBuilder.RenameIndex(
                name: "IX_serverPlayerConnections_serverRecordid",
                table: "serverPlayerConnections",
                newName: "IX_serverPlayerConnections_server_id");

            migrationBuilder.RenameIndex(
                name: "IX_serverPlayerConnections_playerRecordbiIdentity",
                table: "serverPlayerConnections",
                newName: "IX_serverPlayerConnections_bi_identity");

            migrationBuilder.RenameColumn(
                name: "biIdentity",
                table: "playerRecords",
                newName: "bi_identity");

            migrationBuilder.RenameColumn(
                name: "playerRecordBiIdentity",
                table: "playerNames",
                newName: "bi_identity");

            migrationBuilder.RenameIndex(
                name: "IX_playerNames_playerRecordBiIdentity_name",
                table: "playerNames",
                newName: "IX_playerNames_bi_identity_name");

            migrationBuilder.RenameColumn(
                name: "ipAddress",
                table: "playerIpAddresses",
                newName: "ip_address");

            migrationBuilder.RenameColumn(
                name: "playerRecordBiIdentity",
                table: "playerIpAddresses",
                newName: "bi_identity");

            migrationBuilder.RenameIndex(
                name: "IX_playerIpAddresses_playerRecordBiIdentity_ipAddress",
                table: "playerIpAddresses",
                newName: "IX_playerIpAddresses_bi_identity_ip_address");

            migrationBuilder.AddForeignKey(
                name: "FK_playerIpAddresses_playerRecords_bi_identity",
                table: "playerIpAddresses",
                column: "bi_identity",
                principalTable: "playerRecords",
                principalColumn: "bi_identity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playerNames_playerRecords_bi_identity",
                table: "playerNames",
                column: "bi_identity",
                principalTable: "playerRecords",
                principalColumn: "bi_identity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serverPlayerConnections_playerRecords_bi_identity",
                table: "serverPlayerConnections",
                column: "bi_identity",
                principalTable: "playerRecords",
                principalColumn: "bi_identity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serverPlayerConnections_serverRecords_server_id",
                table: "serverPlayerConnections",
                column: "server_id",
                principalTable: "serverRecords",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerIpAddresses_playerRecords_bi_identity",
                table: "playerIpAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_playerNames_playerRecords_bi_identity",
                table: "playerNames");

            migrationBuilder.DropForeignKey(
                name: "FK_serverPlayerConnections_playerRecords_bi_identity",
                table: "serverPlayerConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_serverPlayerConnections_serverRecords_server_id",
                table: "serverPlayerConnections");

            migrationBuilder.RenameColumn(
                name: "ip_address",
                table: "serverRecords",
                newName: "ipAddress");

            migrationBuilder.RenameIndex(
                name: "IX_serverRecords_ip_address_port",
                table: "serverRecords",
                newName: "IX_serverRecords_ipAddress_port");

            migrationBuilder.RenameColumn(
                name: "connection_time",
                table: "serverPlayerConnections",
                newName: "connectionTime");

            migrationBuilder.RenameColumn(
                name: "server_id",
                table: "serverPlayerConnections",
                newName: "serverRecordid");

            migrationBuilder.RenameColumn(
                name: "bi_identity",
                table: "serverPlayerConnections",
                newName: "playerRecordbiIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_serverPlayerConnections_server_id",
                table: "serverPlayerConnections",
                newName: "IX_serverPlayerConnections_serverRecordid");

            migrationBuilder.RenameIndex(
                name: "IX_serverPlayerConnections_bi_identity",
                table: "serverPlayerConnections",
                newName: "IX_serverPlayerConnections_playerRecordbiIdentity");

            migrationBuilder.RenameColumn(
                name: "bi_identity",
                table: "playerRecords",
                newName: "biIdentity");

            migrationBuilder.RenameColumn(
                name: "bi_identity",
                table: "playerNames",
                newName: "playerRecordBiIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_playerNames_bi_identity_name",
                table: "playerNames",
                newName: "IX_playerNames_playerRecordBiIdentity_name");

            migrationBuilder.RenameColumn(
                name: "ip_address",
                table: "playerIpAddresses",
                newName: "ipAddress");

            migrationBuilder.RenameColumn(
                name: "bi_identity",
                table: "playerIpAddresses",
                newName: "playerRecordBiIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_playerIpAddresses_bi_identity_ip_address",
                table: "playerIpAddresses",
                newName: "IX_playerIpAddresses_playerRecordBiIdentity_ipAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_playerIpAddresses_playerRecords_playerRecordBiIdentity",
                table: "playerIpAddresses",
                column: "playerRecordBiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playerNames_playerRecords_playerRecordBiIdentity",
                table: "playerNames",
                column: "playerRecordBiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serverPlayerConnections_playerRecords_playerRecordbiIdentity",
                table: "serverPlayerConnections",
                column: "playerRecordbiIdentity",
                principalTable: "playerRecords",
                principalColumn: "biIdentity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serverPlayerConnections_serverRecords_serverRecordid",
                table: "serverPlayerConnections",
                column: "serverRecordid",
                principalTable: "serverRecords",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
