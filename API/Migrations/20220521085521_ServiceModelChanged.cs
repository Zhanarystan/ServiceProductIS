using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ServiceModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "KzName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RuName",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KzName",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuName",
                table: "Services",
                type: "text",
                nullable: true);
        }
    }
}
