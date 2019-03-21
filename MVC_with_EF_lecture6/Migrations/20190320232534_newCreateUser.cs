using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA.Migrations
{
    public partial class newCreateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerConfirmedPassword",
                table: "Player",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerConfirmedPassword",
                table: "Player");
        }
    }
}
