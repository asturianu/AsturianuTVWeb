using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class DeletePropertiesFromTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Matches",
                type: "int",
                nullable: true);
        }
    }
}
