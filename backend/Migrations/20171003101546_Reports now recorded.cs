using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BonelessPharmacyBackend.Migrations
{
    public partial class Reportsnowrecorded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFiles");
        }
    }
}
