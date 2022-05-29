using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class UserFieldPhoneNumberAndOnDeleteMaterialAndNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogMaterial_Materials_MaterialId",
                table: "BlogMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsMaterial_Materials_MaterialId",
                table: "NewsMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsMaterial_News_NewsId",
                table: "NewsMaterial");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogMaterial_Materials_MaterialId",
                table: "BlogMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsMaterial_Materials_MaterialId",
                table: "NewsMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsMaterial_News_NewsId",
                table: "NewsMaterial",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogMaterial_Materials_MaterialId",
                table: "BlogMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsMaterial_Materials_MaterialId",
                table: "NewsMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsMaterial_News_NewsId",
                table: "NewsMaterial");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogMaterial_Materials_MaterialId",
                table: "BlogMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsMaterial_Materials_MaterialId",
                table: "NewsMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsMaterial_News_NewsId",
                table: "NewsMaterial",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
