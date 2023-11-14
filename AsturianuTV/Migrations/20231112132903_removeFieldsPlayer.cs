using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class removeFieldsPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matches",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Matches",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
