using Microsoft.EntityFrameworkCore.Migrations;

namespace AsturianuTV.Migrations
{
    public partial class HeroeSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CoolDown",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManaCost",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillType",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeDuration",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Armor",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attribute",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthRegeneration",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicResist",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mana",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManaRegeneration",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MoveSpeed",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangeType",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoolDown",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ManaCost",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillType",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "TimeDuration",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Armor",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Attribute",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "HealthRegeneration",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "MagicResist",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Mana",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ManaRegeneration",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "MoveSpeed",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RangeType",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Skills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
