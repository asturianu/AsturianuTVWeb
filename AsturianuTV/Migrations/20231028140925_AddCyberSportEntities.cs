using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class AddCyberSportEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "News",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PriceFound = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    FullPrice = table.Column<int>(nullable: false),
                    Matches = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeagueTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueTeams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    Bday = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    FullPrice = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    Matches = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_LeagueId",
                table: "News",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_News_PlayerId",
                table: "News",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_News_TeamId",
                table: "News",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTeams_LeagueId",
                table: "LeagueTeams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTeams_TeamId",
                table: "LeagueTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Leagues_LeagueId",
                table: "News",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Players_PlayerId",
                table: "News",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Teams_TeamId",
                table: "News",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Leagues_LeagueId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Players_PlayerId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Teams_TeamId",
                table: "News");

            migrationBuilder.DropTable(
                name: "LeagueTeams");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_News_LeagueId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_PlayerId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_TeamId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "News");
        }
    }
}
