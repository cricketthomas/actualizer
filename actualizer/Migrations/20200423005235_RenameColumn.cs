using Microsoft.EntityFrameworkCore.Migrations;

namespace actualizer.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchResultsMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    totalSearches = table.Column<int>(nullable: false),
                    totalCommentsSearched = table.Column<int>(nullable: false),
                    keywordsExtracted = table.Column<int>(nullable: false),
                    sentimentAPIRequests = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResultsMetadata", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResultsMetadata");
        }
    }
}
