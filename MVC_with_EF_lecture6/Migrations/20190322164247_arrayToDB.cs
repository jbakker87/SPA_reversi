using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA.Migrations
{
    public partial class arrayToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "boardString",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "boardString",
                table: "Game");
        }
    }
}
