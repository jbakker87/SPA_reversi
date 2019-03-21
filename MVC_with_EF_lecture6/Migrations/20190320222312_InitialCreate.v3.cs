using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA.Migrations
{
    public partial class InitialCreatev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Player",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Player",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
