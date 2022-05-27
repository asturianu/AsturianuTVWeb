using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class NewsMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Materials",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Materials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_NewsId",
                table: "Materials",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_News_NewsId",
                table: "Materials",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_News_NewsId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_NewsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Materials");
        }
    }
}
