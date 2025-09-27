using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "playerRecords",
                columns: table => new
                {
                    biIdentity = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerRecords", x => x.biIdentity);
                });

            migrationBuilder.CreateTable(
                name: "serverRecords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ipAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    port = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serverRecords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "playerIpAddresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playerRecordbiIdentity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ipAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerIpAddresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_playerIpAddresses_playerRecords_playerRecordbiIdentity",
                        column: x => x.playerRecordbiIdentity,
                        principalTable: "playerRecords",
                        principalColumn: "biIdentity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playerNames",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playerRecordbiIdentity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerNames", x => x.id);
                    table.ForeignKey(
                        name: "FK_playerNames_playerRecords_playerRecordbiIdentity",
                        column: x => x.playerRecordbiIdentity,
                        principalTable: "playerRecords",
                        principalColumn: "biIdentity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serverPlayerConnections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serverRecordid = table.Column<int>(type: "int", nullable: false),
                    playerRecordbiIdentity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    connectionTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serverPlayerConnections", x => x.id);
                    table.ForeignKey(
                        name: "FK_serverPlayerConnections_playerRecords_playerRecordbiIdentity",
                        column: x => x.playerRecordbiIdentity,
                        principalTable: "playerRecords",
                        principalColumn: "biIdentity",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_serverPlayerConnections_serverRecords_serverRecordid",
                        column: x => x.serverRecordid,
                        principalTable: "serverRecords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_playerIpAddresses_playerRecordbiIdentity",
                table: "playerIpAddresses",
                column: "playerRecordbiIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_playerNames_playerRecordbiIdentity",
                table: "playerNames",
                column: "playerRecordbiIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_serverPlayerConnections_playerRecordbiIdentity",
                table: "serverPlayerConnections",
                column: "playerRecordbiIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_serverPlayerConnections_serverRecordid",
                table: "serverPlayerConnections",
                column: "serverRecordid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playerIpAddresses");

            migrationBuilder.DropTable(
                name: "playerNames");

            migrationBuilder.DropTable(
                name: "serverPlayerConnections");

            migrationBuilder.DropTable(
                name: "playerRecords");

            migrationBuilder.DropTable(
                name: "serverRecords");
        }
    }
}
