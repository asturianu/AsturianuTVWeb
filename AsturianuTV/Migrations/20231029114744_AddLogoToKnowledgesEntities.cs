using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class AddLogoToKnowledgesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Leagues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Leagues");
        }
    }
}
