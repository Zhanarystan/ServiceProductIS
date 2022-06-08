using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Modified2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KzName",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "RuName",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "KzName",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "RuName",
                table: "Manufacturers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KzName",
                table: "Metrics",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuName",
                table: "Metrics",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KzName",
                table: "Manufacturers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuName",
                table: "Manufacturers",
                type: "text",
                nullable: true);
        }
    }
}
