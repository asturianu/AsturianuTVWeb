using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class OpenDotaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_RadiantTeamId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "OpenDotaId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Games = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerHistories_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerHistories_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistories_PlayerId",
                table: "PlayerHistories",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHistories_TeamId",
                table: "PlayerHistories",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_RadiantTeamId",
                table: "Matches",
                column: "RadiantTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_RadiantTeamId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "PlayerHistories");

            migrationBuilder.DropColumn(
                name: "OpenDotaId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_RadiantTeamId",
                table: "Matches",
                column: "RadiantTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
