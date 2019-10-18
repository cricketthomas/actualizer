using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace actualizer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedObjects",
                columns: table => new
                {
                    VideoId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Object = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedObjects", x => x.VideoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedObjects");
        }
    }
}
