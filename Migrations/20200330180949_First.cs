using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace filemove.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileArchives",
                columns: table => new
                {
                    FileArchiveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderPath = table.Column<string>(nullable: true),
                    ArchiveName = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileArchives", x => x.FileArchiveId);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveLog",
                columns: table => new
                {
                    ArchiveLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalPath = table.Column<string>(nullable: true),
                    DestinationPath = table.Column<string>(nullable: true),
                    ArchiveDate = table.Column<DateTime>(nullable: false),
                    FileArchiveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveLog", x => x.ArchiveLogId);
                    table.ForeignKey(
                        name: "FK_ArchiveLog_FileArchives_FileArchiveId",
                        column: x => x.FileArchiveId,
                        principalTable: "FileArchives",
                        principalColumn: "FileArchiveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveLog_FileArchiveId",
                table: "ArchiveLog",
                column: "FileArchiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveLog");

            migrationBuilder.DropTable(
                name: "FileArchives");
        }
    }
}
