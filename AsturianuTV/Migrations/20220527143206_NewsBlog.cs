using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class NewsBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Blogs_BlogId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_News_NewsId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_BlogId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_NewsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Materials");

            migrationBuilder.AddColumn<bool>(
                name: "IsNewsMaterial",
                table: "Materials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BlogMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: true),
                    MaterialId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogMaterial_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BlogMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(nullable: true),
                    MaterialId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsMaterial_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogMaterial_BlogId",
                table: "BlogMaterial",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogMaterial_MaterialId",
                table: "BlogMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsMaterial_MaterialId",
                table: "NewsMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsMaterial_NewsId",
                table: "NewsMaterial",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogMaterial");

            migrationBuilder.DropTable(
                name: "NewsMaterial");

            migrationBuilder.DropColumn(
                name: "IsNewsMaterial",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_BlogId",
                table: "Materials",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_NewsId",
                table: "Materials",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Blogs_BlogId",
                table: "Materials",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_News_NewsId",
                table: "Materials",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
