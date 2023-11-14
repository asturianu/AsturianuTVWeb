using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class ChangeRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatchStats_MatchResults_MatchResultId",
                table: "PlayerMatchStats");

            migrationBuilder.DropTable(
                name: "MatchResults");

            migrationBuilder.DropIndex(
                name: "IX_PlayerMatchStats_MatchResultId",
                table: "PlayerMatchStats");

            migrationBuilder.DropColumn(
                name: "MatchResultId",
                table: "PlayerMatchStats");

            migrationBuilder.AddColumn<bool>(
                name: "IsRadiantWin",
                table: "Matches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_HeroId",
                table: "PlayerMatchStats",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatchStats_Characters_HeroId",
                table: "PlayerMatchStats",
                column: "HeroId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatchStats_Characters_HeroId",
                table: "PlayerMatchStats");

            migrationBuilder.DropIndex(
                name: "IX_PlayerMatchStats_HeroId",
                table: "PlayerMatchStats");

            migrationBuilder.DropColumn(
                name: "IsRadiantWin",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "MatchResultId",
                table: "PlayerMatchStats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    WinningTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchResults_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchResults_Teams_WinningTeamId",
                        column: x => x.WinningTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_MatchResultId",
                table: "PlayerMatchStats",
                column: "MatchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_MatchId",
                table: "MatchResults",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_WinningTeamId",
                table: "MatchResults",
                column: "WinningTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatchStats_MatchResults_MatchResultId",
                table: "PlayerMatchStats",
                column: "MatchResultId",
                principalTable: "MatchResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
